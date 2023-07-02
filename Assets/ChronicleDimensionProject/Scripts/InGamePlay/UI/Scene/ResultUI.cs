using UniRx;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResultUI : UIBase
{
    // Result UIの実装
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _experienceText;
    [SerializeField] private TextMeshProUGUI _levelText;

    private void Update()
    {
        _scoreText.text = $"Score:{GameManager.Instance.ScoreManager.Score.Value}";
        _coinText.text = $"Coin:{GameManager.Instance.ScoreManager.Coin.Value}";
        _experienceText.text = $"Experience:{GameManager.Instance.LevelUpManager.Experience.Value}";
        _levelText.text = $"Level:{GameManager.Instance.LevelUpManager.CurrentLevel.Value}";
    }
}