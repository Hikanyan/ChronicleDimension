using UnityEngine;
using UniRx;

public class RhythmGameScore : ScoreController
{
    [HideInInspector] public int sum = 0;
    [HideInInspector] public int combo = 0;
    private int _maxCombo = 0;

    public JudgeScores _judgeScores;

    public int MaxScore;
    public int SingleScore;
    public int NotesNum;
    public float clearPercent;

    public void EvaluateNote(Judges judge)
    {
        float scorePercent = 1.0f;
        switch (judge)
        {
            case Judges.PurePerfect:
                scorePercent = 1.0f;
                combo++;
                break;
            case Judges.Perfect:
                scorePercent = 0.9f;
                combo++;
                break;
            case Judges.Great:
                scorePercent = 0.8f;
                combo++;
                break;
            case Judges.Good:
                scorePercent = 0.5f;
                combo++;
                break;
            case Judges.Bad:
                scorePercent = 0.3f;
                combo = 0;
                break;
            case Judges.Miss:
                scorePercent = 0.0f;
                combo = 0;
                break;
            default:
                break;
        }

        _maxCombo = Mathf.Max(_maxCombo, combo);
        AddToScore(Mathf.FloorToInt(scorePercent * SingleScore));
    }

    private void AddToScore(int points)
    {
        AddScore(points);
    }

    public void SetMaxScore()
    {
        Debug.Log(NotesNum);
        if (NotesNum == 0) return;
        MaxScore = 10000000 + NotesNum;
        SingleScore = MaxScore / NotesNum;
    }

    public void Combo(bool isCombo)
    {
        if (isCombo)
        {
            combo++;
        }
        else
        {
            combo = 0;
        }

        _maxCombo = Mathf.Max(_maxCombo, combo);
    }

    public string GetRank()
    {
        clearPercent = (float)Score.Value / MaxScore;
        if (_judgeScores.PurePerfect >= NotesNum) return "APP";
        if (_judgeScores.Perfect + _judgeScores.PurePerfect >= NotesNum) return "AP";
        if (clearPercent >= 1.00f) return "EX";
        if (clearPercent >= 0.99f) return "SSS";
        if (clearPercent >= 0.98f) return "SS";
        if (clearPercent >= 0.97f) return "S";
        if (clearPercent >= 0.96f) return "AAA";
        if (clearPercent >= 0.95f) return "AA";
        if (clearPercent >= 0.94f) return "A";
        if (clearPercent >= 0.93f) return "BBB";
        if (clearPercent >= 0.90f) return "BB";
        if (clearPercent >= 0.80f) return "B";
        if (clearPercent >= 0.70f) return "CCC";
        if (clearPercent >= 0.60f) return "CC";
        if (clearPercent >= 0.50f) return "C";
        return "D";
    }

    public ResultDatas GetResultData()
    {
        ResultDatas resultDatas = new ResultDatas();
        resultDatas.Rank = GetRank();
        resultDatas.ClearPercent = clearPercent;
        resultDatas.Score = Score.Value;
        resultDatas._judgeScores = _judgeScores;
        resultDatas._maxCombo = _maxCombo;
        return resultDatas;
    }
}