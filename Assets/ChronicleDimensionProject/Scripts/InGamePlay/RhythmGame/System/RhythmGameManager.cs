using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

enum RhythmGameState
{
    None, // ゲームが開始されていない状態
    Start, // ゲームが開始されたが、プレイヤーが操作を開始していない状態
    Playing, // ゲームが進行中で、プレイヤーが楽曲に対応した操作を行っている状態
    Pause, // ゲームが一時停止中で、プレイヤーが操作を中断している状態
    Finish, // ゲームが終了した状態
    GameOver // ゲームが終了し、プレイヤーが失敗した状態
}


public class RhythmGameManager : AbstractSingleton<RhythmGameManager>
{
    [Header("UI")] [SerializeField] GameObject hud = default;
    [SerializeField] GameObject musicPreviewUI = default;
    [SerializeField] GameObject fpsUI = default;

    [Header("GameMasterSetting")] [SerializeField]
    float waitToGameEndTime = 3.0f;

    [Header("GetOpenMusic")] [SerializeField]
    AssetReferenceT<TextAsset> musicJsonReference;

    [SerializeField] CRIAudioList criAudioNumber;
    [SerializeField] float delayTime = 0.0f;
    bool showFPS = false;

    private ScoreManager _scoreManager;
    private TimerManager _timerManager;

    protected override void OnAwake()
    {
        Application.targetFrameRate = 0;
    }

    void Start()
    {
        
        _scoreManager = new ScoreManager();
        _timerManager = new TimerManager();
    }

    void Update()
    {
    }

    void GameStart()
    {
        _timerManager.TimerStart();
        CRIAudioManager.Instance.CribgmPlay(criAudioNumber, delayTime);
    }


    public void AddScore(int points)
    {
        _scoreManager.AddScore(points);
    }

    public void ResetScore()
    {
        _scoreManager.ResetScore();
    }
}