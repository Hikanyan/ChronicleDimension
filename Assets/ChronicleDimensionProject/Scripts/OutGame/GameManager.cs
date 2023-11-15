using System;
using ChronicleDimension.Core;
using ChronicleDimension.InGame.ActionGame;
using ChronicleDimension.InGame.NovelGame;
using ChronicleDimensionProject.Player;
using UnityEngine;
using State = StateMachine<ChronicleDimensionProject.Scripts.OutGame.GameManager>.State;

namespace ChronicleDimensionProject.Scripts.OutGame
{
    /// <summary>
    /// ゲーム全体の管理
    /// </summary>
    [Serializable]
    public class GameManager : AbstractSingleton<GameManager>
    {
        public StateMachine<GameManager> stateMachine;
        public GameState CurrentGameState { get; private set; }

        public RhythmGameManager rhythmGameManager;
        public ActionGameManager actionGameManager;
        public NovelGameManager novelGameManager;

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

        protected override void OnAwake()
        {
            // 各Singletonクラスの初期化
            sceneController = SceneController.Instance;
            uiManager = UIManager.Instance;
            audioManager = CriAudioManager.Instance;
            saveManager = SaveManager.Instance;
            rhythmGameManager = RhythmGameManager.Instance;
            actionGameManager = ActionGameManager.Instance;
            novelGameManager = NovelGameManager.Instance;

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
                await SceneController.Instance.LoadScene("TitleScene");
            }

            protected override void OnUpdate()
            {
            }

            protected override void OnExit(State nextState)
            {
            }
        }
    }
}
