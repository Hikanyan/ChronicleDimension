using System;
using UniRx;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private double _startTime = 0;
    private double _pauseStartTime = 0;
    private double _accumulatedGameTime = 0;

    /// <summary> リアルの経過時間 </summary>
    public ReactiveProperty<double> RealTime { get; private set; } = new ReactiveProperty<double>();
    /// <summary> ゲームの経過時間 </summary>
    public ReactiveProperty<double> GameTime { get; private set; } = new ReactiveProperty<double>();
    /// <summary> タイマーが動いているかどうか </summary>
    public BoolReactiveProperty IsRunning { get; private set; } = new BoolReactiveProperty(false);
    /// <summary> 現在の日時 </summary>
    private ReactiveProperty<DateTime> CurrentDateTime { get; set; } = new ReactiveProperty<DateTime>(DateTime.Now);
    
    public event Action TimerStarted;
    public event Action TimerPaused;
    public event Action TimerUnpaused;
    public event Action TimerStopped;

    private void Update()
    {
        UpdateTimes();
    }

    public void TimerStart()
    {
        IsRunning.Value = true;
        _startTime = Time.realtimeSinceStartupAsDouble;
        TimerStarted?.Invoke();
    }

    public void TimerPause()
    {
        if (IsRunning.Value)
        {
            _pauseStartTime = Time.realtimeSinceStartupAsDouble;
            _accumulatedGameTime += _pauseStartTime - _startTime;
        }
        IsRunning.Value = false;
        TimerPaused?.Invoke();
    }

    public void TimerUnPause()
    {
        if (!IsRunning.Value)
        {
            _startTime = Time.realtimeSinceStartupAsDouble;
        }
        IsRunning.Value = true;
        TimerUnpaused?.Invoke();
    }

    public void TimerStop()
    {
        IsRunning.Value = false;
        _accumulatedGameTime = 0;
        TimerStopped?.Invoke();
    }

    private void UpdateTimes()
    {
        CurrentDateTime.Value = DateTime.Now;
        RealTime.Value = Time.realtimeSinceStartupAsDouble - _startTime;
        if (IsRunning.Value)
        {
            GameTime.Value = Time.realtimeSinceStartupAsDouble - _startTime + _accumulatedGameTime;
        }
    }
}