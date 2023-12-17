using ChronicleDimensionProject.Player;
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
        return clearPercent switch
        {
            >= 1.00f => "EX",
            >= 0.99f => "SSS",
            >= 0.98f => "SS",
            >= 0.97f => "S",
            >= 0.96f => "AAA",
            >= 0.95f => "AA",
            >= 0.94f => "A",
            >= 0.93f => "BBB",
            >= 0.90f => "BB",
            >= 0.80f => "B",
            >= 0.70f => "CCC",
            >= 0.60f => "CC",
            >= 0.50f => "C",
            _ => "D"
        };
    }

    public RhythmGameResultDatas GetResultData()
    {
        RhythmGameResultDatas resultDatas = new RhythmGameResultDatas();
        resultDatas.rank = GetRank();
        resultDatas.clearPercent = clearPercent;
        resultDatas.score = Score.Value;
        resultDatas.judgeScores = _judgeScores;
        resultDatas.maxCombo = _maxCombo;
        return resultDatas;
    }
}