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
    RhythmGameManager _rhythmGameManager;
    GameState _gameState = GameState.None;

    protected override void OnAwake()
    {
        
    }
    private void Update()
    {
        
    }



    /// <summary>
    /// Sceneの初期化を行います。
    /// </summary>
    public void Initialize()
    {
        _sceneController = new SceneController(SceneManager.GetActiveScene());
        _rhythmGameManager = new RhythmGameManager();
    }

    /// <summary>
    /// 指定したシーンを非同期でロードします。
    /// </summary>
    /// <param name="scene"></param>
    public async UniTask LoadScene(string scene)
    {
        await _sceneController.LoadScene(scene);
    }

    /// <summary>
    /// LoadSceneとの違いは、こちらはシーンをアンロードせずに新しいシーンをロードすることです。
    /// </summary>
    /// <param name="scene"></param>
    public async UniTask LoadNewScene(string scene)
    {
        await _sceneController.LoadNewScene(scene);
    }

    public async UniTask UnloadLastScene()
    {
        await _sceneController.UnloadLastScene();
    }
}