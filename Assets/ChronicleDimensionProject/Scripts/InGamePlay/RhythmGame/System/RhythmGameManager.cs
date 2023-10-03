using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

[RequireComponent(typeof(TimerManager), typeof(ScoreManager))]
public class RhythmGameManager : AbstractSingleton<RhythmGameManager>
{
    [Header("UI")] [SerializeField] GameObject hudUI = default;
    [SerializeField] GameObject musicPreviewUI = default;
    [SerializeField] GameObject fpsUI = default;

    [Header("GameMasterSetting")] [SerializeField]
    float waitToGameEndTime = 3.0f;

    [Header("GetOpenMusic")] [SerializeField]
    AssetReferenceT<TextAsset> musicJsonReference;

    CriAudioList _criAudioNumber;
    float _delayTime = 0.0f;
    bool _showFPS = false;

    private TimerManager _timerManager;
    private ScoreManager _scoreManager;


    private IntReactiveProperty _playerLevel = new(1);
    private IntReactiveProperty _money = new(0);

    public IReadOnlyReactiveProperty<int> PlayerLevel => _playerLevel;
    public IReadOnlyReactiveProperty<int> Money => _money;

    protected override void OnAwake()
    {
        Application.targetFrameRate = 0;
        UIManager.Instance.AddUI<RhythmGame>(hudUI);
        UIManager.Instance.AddUI<RhythmGame>(musicPreviewUI);
        UIManager.Instance.AddUI<RhythmGame>(fpsUI);

    }

    void Start()
    {
        TryGetComponent(out _timerManager);
        TryGetComponent(out _scoreManager);
        OpenMusic openMusic = GameObject.FindObjectOfType<OpenMusic>();

        if (openMusic != null)
        {
            _criAudioNumber = openMusic.musicData.musicNumber;
            musicJsonReference = openMusic.musicData.musicJsonReference;
            _delayTime = openMusic.musicData.delayTime;
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
        _timerManager.TimerStart();
        //CRIAudioManager.Instance.PlayBGM();
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