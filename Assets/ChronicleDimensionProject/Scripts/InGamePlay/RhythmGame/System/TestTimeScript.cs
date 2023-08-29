using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TestTimeScript : MonoBehaviour
{
    private TimerManager timerManager;

    private void Start()
    {
        timerManager = new TimerManager();

        
        // タイマーのリアルタイム値と状態を出力するサンプル
        timerManager.RealTime.Subscribe(value => Debug.Log($"RealTime: {(float)value}, IsRunning: {timerManager.IsRunning.Value}")).AddTo(this);
        // タイマーを開始
        timerManager.TimerStart();

        // タイマーを2秒後に一時停止し、3秒後に再開
        Observable.Timer(TimeSpan.FromSeconds(2))
            .Subscribe(_ =>
            {
                timerManager.TimerPause();
                Debug.Log("Timer paused.");
            })
            .AddTo(this);

        Observable.Timer(TimeSpan.FromSeconds(5))
            .Subscribe(_ =>
            {
                timerManager.TimerUnPause();
                Debug.Log("Timer unpaused.");
            })
            .AddTo(this);

        // タイマーを停止
        Observable.Timer(TimeSpan.FromSeconds(7))
            .Subscribe(_ =>
            {
                timerManager.TimerStop();
                Debug.Log("Timer stopped.");
            })
            .AddTo(this);
    }
}