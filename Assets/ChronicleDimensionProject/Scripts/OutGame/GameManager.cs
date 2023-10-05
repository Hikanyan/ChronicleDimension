using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using State = StateMachine<GameManager>.State;
[Serializable]
public class GameManager : AbstractSingleton<GameManager>
{
    public StateMachine<GameManager> stateMachine;
    public GameState CurrentGameState { get; private set; }
    
    // ゲーム開始時に実行されるイベント
    public event Action OnGameStart;
    protected override void OnAwake()
    {
        Initialize();
    }

    public void Initialize()
    {
        stateMachine = new StateMachine<GameManager>(this);
        stateMachine.Start<TitleState>();
    }

    

    public async UniTask ChangeGameState(GameState newState)
    {
        // 既存の状態から新しい状態への遷移処理を記述
        switch (newState)
        {
            case GameState.Title:
                // Title画面の初期化や表示処理を行う
                await SceneController.Instance.LoadScene("TitleScene");
                await UIManager.Instance.OpenUI<Title>();
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
    
    
    private class TitleState : State
    {
        protected override async void OnEnter(State prevState)
        {
            // タイトルステートに入った時の処理
            await SceneController.Instance.LoadScene("TitleScene");
            await UIManager.Instance.OpenUI<Title>();
        }

        protected override void OnUpdate()
        {
            // タイトルステートの更新処理
        }

        protected override async void OnExit(State nextState)
        {
            // タイトルステートから出た時の処理
            UIManager.Instance.CloseUI<Title>();
        }
    }

    private void OnOnGameStart()
    {
        OnGameStart?.Invoke();
    }
}

