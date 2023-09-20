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
    public GameState CurrentGameState { get; private set; }
    

    protected override void OnAwake()
    {
        Initialize();
    }

    public void Initialize()
    {
        ChangeGameState(GameState.Title);
    }

    

    public async UniTask ChangeGameState(GameState newState)
    {
        // 既存の状態から新しい状態への遷移処理を記述
        switch (newState)
        {
            case GameState.Title:
                // Title画面の初期化や表示処理を行う
                await SceneController.Instance.LoadScene(CurrentGameState.ToString());
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

