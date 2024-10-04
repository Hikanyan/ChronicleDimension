using UnityEngine;

namespace ChronicleDimensionProject.RhythmGame.JudgeData
{
    public enum Judges
    {
        PurePerfect,
        Perfect,
        Great,
        Good,
        Bad,
        Miss,
        Auto,
        None,
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

    public struct JudgeTime
    {
        #region JudgeTimes

        private const float PurePerfectTime = 0.01f;
        private const float PerfectTime = 0.05f;
        private const float GreatTime = 0.1f;
        private const float GoodTime = 0.15f;
        public const float BadTime = 0.20f;

        #endregion

        public static Judges JudgeNotes(float time)
        {
            time = Mathf.Abs(time);
            return time switch
            {
                <= PurePerfectTime => Judges.PurePerfect,
                <= PerfectTime => Judges.Perfect,
                <= GreatTime => Judges.Great,
                <= GoodTime => Judges.Good,
                <= BadTime => Judges.Bad,
                _ => Judges.None
            };
        }
    }

    public struct ResultData
    {
        public JudgeScores JudgeScores;
        public int MaxCombo;
        public int Score;
        public float ClearPercent;
        public string MusicName;
        public string Rank;
    }
}