using System;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Threading;

public class TimerManager
{
    private FloatReactiveProperty _timer = new FloatReactiveProperty(0f);
    private CancellationTokenSource _cancellationTokenSource;

    public IReadOnlyReactiveProperty<float> Timer => _timer;

    public async UniTask StartTimer(float duration)
    {
        // タイマーが既に実行中であれば中止する
        if (_cancellationTokenSource != null)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        _timer.Value = duration;

        // CancellationTokenSource を作成してタイマーを実行
        _cancellationTokenSource = new CancellationTokenSource();
        try
        {
            await UniTask.Delay((int)(duration * 1000), cancellationToken: _cancellationTokenSource.Token);
        }
        catch (OperationCanceledException)
        {
            // タイマーが中断された場合はここで処理
            return;
        }

        _timer.Value = 0f;
    }

    public void ResetTimer()
    {
        // タイマーが既に実行中であれば中止する
        if (_cancellationTokenSource != null)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }
        
        _timer.Value = 0f;
    }

    public void StopTimer()
    {
        // タイマーが既に実行中であれば中止する
        if (_cancellationTokenSource != null)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }
    }
}