using System.Collections;
using System.Collections.Generic;
using ChronicleDimension.Core;
using ChronicleDimensionProject.Scripts.Core.UI;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

[RequireComponent(typeof(TimerManager), typeof(RhythmGameScore))]
public class RhythmGameManager : AbstractSingleton<RhythmGameManager>
{
    [Header("UI")] [SerializeField] GameObject hudUI = default;
    [SerializeField] GameObject musicPreviewUI = default;
    [SerializeField] GameObject fpsUI = default;

    [Header("GameMasterSetting")] [SerializeField]
    float waitToGameEndTime = 3.0f;

    [Header("GetOpenMusic")] [SerializeField]
    AssetReferenceT<TextAsset> musicJsonReference;

    CriAudioManager.CueSheet _cueSheet;
    string _musicName;

    float _delayTime = 0.0f;
    bool _showFPS = false;

    public bool AutoMode { get; set; } = false;

    public TimerManager timerManager;
    [FormerlySerializedAs("rhythmGameScoreManager")] public RhythmGameScore rhythmGameScore;


    private IntReactiveProperty _playerLevel = new(1);
    private IntReactiveProperty _money = new(0);

    public IReadOnlyReactiveProperty<int> PlayerLevel => _playerLevel;
    public IReadOnlyReactiveProperty<int> Money => _money;

    protected override void OnAwake()
    {
        Application.targetFrameRate = 0;
        //UIManager.Instance.RegisterPanel(hudUI.GetComponent<IUserInterface>());
    }

    void Start()
    {
        TryGetComponent(out timerManager);
        TryGetComponent(out rhythmGameScore);
        OpenMusic openMusic = GameObject.FindObjectOfType<OpenMusic>();

        if (openMusic != null)
        {
            _musicName = openMusic.musicData.musicName;
            musicJsonReference = openMusic.musicData.musicJsonReference;
            _delayTime = openMusic.musicData.delayTime;
            AutoMode = openMusic.musicData.autoMode;
        }

        SetShowMusicPreviewUI(true);
    }

    void SetShowMusicPreviewUI(bool amount)
    {
        hudUI.SetActive(amount);
        musicPreviewUI.SetActive(amount);
    }

    void Update()
    {
    }

    void GameStart()
    {
        timerManager.TimerStart();
        CriAudioManager.Instance.PlayBGM(_cueSheet, _musicName);
    }

    public void GameEnd()
    {
        timerManager.TimerStop();
    }

    public enum RhythmGameState
    {
        None, // ゲームが開始されていない状態
        Start, // ゲームが開始されたが、プレイヤーが操作を開始していない状態
        Playing, // ゲームが進行中で、プレイヤーが楽曲に対応した操作を行っている状態
        Pause, // ゲームが一時停止中で、プレイヤーが操作を中断している状態
        Finish, // ゲームが終了した状態
        GameOver // ゲームが終了し、プレイヤーが失敗した状態
    }
}