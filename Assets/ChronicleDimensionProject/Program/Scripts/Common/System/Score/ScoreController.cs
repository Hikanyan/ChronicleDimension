using UniRx;
using UnityEngine;

/// <summary>  共通のスコアクラス </summary>
public class ScoreController : MonoBehaviour, IScore
{
    private IntReactiveProperty _score = new IntReactiveProperty(0);

    public IReadOnlyReactiveProperty<int> Score => _score;

    public void AddScore(int points)
    {
        _score.Value += points;
    }

    public void ResetScore()
    {
        _score.Value = 0;
    }
}