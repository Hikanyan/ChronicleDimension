using System;
using ChronicleDimensionProject.InGame.ActionGame;
using ChronicleDimensionProject.InGame.NovelGame;
using ChronicleDimensionProject.Common;
using ChronicleDimensionProject.GameSelectScene;
using ChronicleDimensionProject.Player;
using ChronicleDimensionProject.Program.Scripts.Gacha.Core;
using ChronicleDimensionProject.Scripts.OutGame;
using ChronicleDimensionProject.Title;
using UnityEngine;

namespace ChronicleDimensionProject.Boot
{
    /// <summary>
    /// ゲーム全体の管理
    /// </summary>
    [Serializable]
    public class GameManager : AbstractSingletonMonoBehaviour<GameManager>
    {
        protected override bool UseDontDestroyOnLoad => true;
        public StateMachine<GameManager> stateMachine;
        public GameState CurrentGameState { get; private set; }

        /// <summary> ゲームの状態 </summary>
        public RhythmGameManager rhythmGameManager;

        public ActionGameManager actionGameManager;
        public NovelGameManager novelGameManager;
        public GachaManager gachaManager;

        /// <summary> シーン管理 </summary>
        public SceneController sceneController;

        ///<summary> UI管理 </summary>
        public UIManager uiManager;

        /// <summary> オーディオ管理 </summary>
        public CriAudioManager audioManager;

        /// <summary> セーブ管理 </summary>
        public SaveManager saveManager;

        /// <summary> ゲーム終了の管理 </summary>
        public void ExitGame()
        {
            Application.Quit();
        }

        /// <summary> セーブの管理 </summary>
        public void SaveGame()
        {
            saveManager.SaveGame();
        }

        /// <summary> ロードの管理 </summary>
        public void LoadGame()
        {
            saveManager.LoadGame();
        }

        public override void OnAwake()
        {
            // 各Singletonクラスの初期化
            sceneController = SceneController.Instance;
            uiManager = UIManager.Instance;
            audioManager = CriAudioManager.Instance;
            saveManager = SaveManager.Instance;

            // rhythmGameManager = RhythmGameManager.Instance;
            // actionGameManager = ActionGameManager.Instance;
            // novelGameManager = NovelGameManager.Instance;

            Initialize();
        }

        public void Initialize()
        {
            stateMachine = new StateMachine<GameManager>(this);
            stateMachine.Start<TitleState>();
        }

        public void Change(GameState gameState)
        {
            CurrentGameState = gameState;

            switch (gameState)
            {
                case GameState.None:
                    break;
                case GameState.Title:
                    ChangeState<TitleState>();
                    break;
                case GameState.GameSelect:
                    ChangeState<GameSelectState>();
                    break;
                case GameState.RhythmGame:
                    // ChangeState<RhythmGameState>();
                    break;
                case GameState.ActionGame:
                    // ChangeState<ActionGameState>();
                    break;
                case GameState.NovelGame:
                    // ChangeState<NovelGameState>();
                    break;
                case GameState.Explanation:
                    // ChangeState<ExplanationState>();
                    break;
                case GameState.Gacha:
                    // ChangeState<GachaState>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
            }
        }

        // ジェネリックタイプを使用したステート遷移の実装例
        private void ChangeState<TState>() where TState : StateMachine<GameManager>.State, new()
        {
            var state = stateMachine.GetOrAddState<TState>();
            stateMachine.Change(state); // 実際のステート遷移を行う
        }
    }
}