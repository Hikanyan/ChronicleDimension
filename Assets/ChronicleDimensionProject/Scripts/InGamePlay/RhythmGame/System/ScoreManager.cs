using UniRx;
using UnityEngine;

public class ScoreManager : MonoBehaviour
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