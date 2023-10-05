using System;
using UniRx;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private double _startTime = 0;
    private double _cacheTime = 0;

    public ReactiveProperty<double> RealTime { get; private set; } = new ReactiveProperty<double>();
    public BoolReactiveProperty IsRunning { get; private set; } = new BoolReactiveProperty(false);

    public event Action TimerStarted;
    public event Action TimerPaused;
    public event Action TimerUnpaused;
    public event Action TimerStopped;

    private IDisposable _timerDisposable;
    

    public void TimerStart()
    {
        IsRunning.Value = true;
        _startTime = Time.realtimeSinceStartupAsDouble;
        TimerStarted?.Invoke();

        // 100ミリ秒ごとにUpdateRealTimeメソッドを呼び出す
        _timerDisposable = Observable.Interval(TimeSpan.FromMilliseconds(1))
            .Subscribe(_ => UpdateRealTime(_startTime))
            .AddTo(FindObjectOfType<MainThreadDispatcher>());
    }

    public void TimerPause()
    {
        IsRunning.Value = false;
        TimerPaused?.Invoke();
    }

    public void TimerUnPause()
    {
        IsRunning.Value = true;
        TimerUnpaused?.Invoke();
    }

    public void TimerStop()
    {
        IsRunning.Value = false;
        TimerStopped?.Invoke();
        _timerDisposable?.Dispose();
    }

    private void UpdateRealTime(double startTime)
    {
        if (IsRunning.Value)
        {
            RealTime.Value = Time.realtimeSinceStartupAsDouble - startTime - _cacheTime;
        }
    }
}