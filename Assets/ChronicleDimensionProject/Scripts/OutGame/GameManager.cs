using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine.Serialization;
using State = StateMachine<GameManager>.State;

/// <summary>
/// ゲーム全体の管理(状態間の遷移を制御)
/// シーン管理(シーンの読み込みとアンロードを処理)
/// リソース管理(ロードとキャッシュを管理)
/// 複数のゲームのSingletonを管理
/// UIの管理
/// プレイヤー情報管理
/// オーディオ管理
/// ゲーム終了の管理
/// セーブ/ロードの管理
/// </summary>
[Serializable]
public class GameManager : AbstractSingleton<GameManager>
{
    public StateMachine<GameManager> stateMachine;
    public GameState CurrentGameState { get; private set; }

    public RhythmGameManager rhythmGameManager;

    /// <summary> シーン管理 </summary>
    public SceneController sceneController;

    /// <summary> リソース管理 </summary>
    //public ResourceManager resourceManager;

    ///<summary> UI管理 </summary>
    public UIManager uiManager;

    /// <summary> プレイヤー情報管理 </summary>
    public PlayerInfo playerInfo;

    /// <summary> オーディオ管理 </summary>
    public CriAudioManager audioManager;

    /// <summary> ゲーム終了の管理 </summary>
    public void ExitGame()
    {
        // ゲーム終了処理を実行
        Application.Quit();
    }

    /// <summary> セーブの管理 </summary>
    public void SaveGame()
    {
        // ゲームの状態やプレイヤーデータをセーブ
        // データの保存処理を実行
    }

    /// <summary> ロードの管理 </summary>
    public void LoadGame()
    {
        // セーブされたデータをロードしてゲーム状態を復元
        // データの読み込み処理を実行
    }

    protected override void OnAwake()
    {
        //各Singletonクラスの初期化
        sceneController = SceneController.Instance;
        //resourceManager = ResourceManager.Instance;
        uiManager = UIManager.Instance;
        playerInfo = PlayerInfo.Instance;
        audioManager = CriAudioManager.Instance;
        
        Initialize();
    }

    public void Initialize()
    {
        stateMachine = new StateMachine<GameManager>(this);
        stateMachine.Start<TitleState>();
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
}