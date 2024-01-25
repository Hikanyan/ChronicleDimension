using UniRx;

// 共通のスコアインターフェース
public interface IScore
{
    IReadOnlyReactiveProperty<int> Score { get; }
    void AddScore(int points);
    void ResetScore();
}