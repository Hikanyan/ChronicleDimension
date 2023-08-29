using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

enum RhythmGameState
{
    None,       // ゲームが開始されていない状態
    Start,      // ゲームが開始されたが、プレイヤーが操作を開始していない状態
    Playing,    // ゲームが進行中で、プレイヤーが楽曲に対応した操作を行っている状態
    Pause,      // ゲームが一時停止中で、プレイヤーが操作を中断している状態
    Finish,     // ゲームが終了した状態
    GameOver    // ゲームが終了し、プレイヤーが失敗した状態
}


public class RhythmGameManager : MonoBehaviour
{
    private ScoreManager _scoreManager;
    private TimerManager _timerManager;

    void Start()
    {
        _scoreManager = new ScoreManager();
        _timerManager = new TimerManager();
    }

    void Update()
    {
        
    }
    
    public void AddScore(int points)
    {
        _scoreManager.AddScore(points);
    }

    public void ResetScore()
    {
        _scoreManager.ResetScore();
    }

    
}