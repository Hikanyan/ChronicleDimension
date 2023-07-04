using UniRx;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUI : UIBase
{
    // Game UIの実装
    
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _experienceText;
    [SerializeField] private TextMeshProUGUI _levelText;
    
    private void Start()
    {
        // スコアの変化を監視し、UIに反映する
       GameManager.Instance.ScoreManager.Score.Subscribe(score =>
        {
            _scoreText.text = $"Score:{score.ToString()}";
        }).AddTo(this);

        // コインの変化を監視し、UIに反映する
        GameManager.Instance.ScoreManager.Coin.Subscribe(coin =>
        {
            _coinText.text = $"Coin:{coin.ToString()}";
        }).AddTo(this);
        // 経験値の変化を監視し、UIに反映する
        GameManager.Instance.LevelUpManager.Experience.Subscribe(experience =>
        {
            _experienceText.text = $"Experience:{experience.ToString()}";
        }).AddTo(this);
        GameManager.Instance.LevelUpManager.CurrentLevel.Subscribe(level =>
        {
            _levelText.text = $"Level:{level.ToString()}";
        }).AddTo(this);
    }
}