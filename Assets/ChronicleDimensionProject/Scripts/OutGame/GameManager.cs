using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
/// <summary>
/// 全てのシーンにまたがるゲームの状態を管理します。
/// </summary>
[Serializable]
public class GameManager : AbstractSingleton<GameManager>
{
    SceneController _sceneController;
    ScoreManager _scoreManager;
    TimerManager _timerManager;
    RhythmGameManager _rhythmGameManager;
    
    /// <summary>
    //初期化
    /// </summary>
    public void Initialize()
    {
        _sceneController = new SceneController(SceneManager.GetActiveScene());
    }
    public async UniTask LoadScene(string scene)
    {
        await _sceneController.LoadScene(scene);
    }

    public async UniTask LoadNewScene(string scene)
    {
        await _sceneController.LoadNewScene(scene);
    }

    public async UniTask UnloadLastScene()
    {
        await _sceneController.UnloadLastScene();
    }
    
}