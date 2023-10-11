using UnityEngine;


public enum Judges
{
    None,
    PurePerfect,
    Perfect,
    Great,
    Good,
    Bad,
    Miss,
    Auto,
}
public enum ResultRank
{
    APP,
    AP,
    EX,
    SSS,
    SS,
    S,
    AAA,
    AA,
    A,
    BBB,
    BB,
    B,
    CCC,
    CC,
    C,
    D,
    Auto
}
public struct JudgeScores
{
    public int PurePerfect;
    public int Perfect;
    public int Great;
    public int Good;
    public int Bad;
    public int Miss;
    public int Auto;
}

/// <summary>
/// 判定有効時間
/// </summary>
public struct JudgeTime
{
    #region JudgeTimes
    private const float PurePerfectTime = 0.01f;
    private const float PerfectTime = 0.05f;
    private const float GreatTime = 0.10f;
    private const float GoodTime = 0.15f;
    private const float BadTime = 0.20f;
    #endregion

    public static Judges JudgeNotes(float time)
    {
        time = Mathf.Abs(time);
        if (time <= PurePerfectTime)
        {
            return Judges.PurePerfect;
        }
        else if (time <= PerfectTime)
        {
            return Judges.Perfect;
        }
        else if (time <= GreatTime)
        {
            return Judges.Great;
        }
        else if (time <= GoodTime)
        {
            return Judges.Good;
        }
        else if (time <= BadTime)
        {
            return Judges.Bad;
        }
        return Judges.None;
    }
}