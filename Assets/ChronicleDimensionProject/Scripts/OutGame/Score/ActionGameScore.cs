// アクションゲームのスコア
public class ActionGameScore : ScoreController
{
    public void EnemyDefeated()
    {
        AddScore(50);
    }

    public void CollectItem()
    {
        AddScore(10);
    }
}