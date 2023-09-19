using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine.SceneManagement;

[Serializable]
public class GameManager : AbstractSingleton<GameManager>
{
    public StateMachine<GameManager> stateMachine;
    SceneController _sceneController;
    public GameState CurrentGameState { get; private set; }
    
    private ReactiveProperty<GameObject> titleObject = new ReactiveProperty<GameObject>();

    protected override void OnAwake()
    {
        Initialize();
    }

    public void Initialize()
    {
        _sceneController = new SceneController(SceneManager.GetActiveScene());
        ChangeGameState(GameState.Title);
    }

    public async UniTask LoadSceneAsync(string sceneName)
    {
        // シーンを非同期にロード
        await  _sceneController.LoadNewScene(sceneName);
    }

    private async UniTask UnloadTitleScene()
    {
        if (titleObject.Value != null)
        {
            Destroy(titleObject.Value);
            titleObject.Value = null;
        }
    }

    public async UniTask ChangeGameState(GameState newState)
    {
        // 既存の状態から新しい状態への遷移処理を記述
        switch (newState)
        {
            case GameState.Title:
                // Title画面の初期化や表示処理を行う
                await LoadSceneAsync(newState.ToString());
                break;
            case GameState.GameStart:
                // ゲームを開始するための初期化処理を行う
                break;
            // 他のゲーム状態についても同様のアプローチで処理を追加
            default:
                break;
        }

        CurrentGameState = newState;
    }
}

