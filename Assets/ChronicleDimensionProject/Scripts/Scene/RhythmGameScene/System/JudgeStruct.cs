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
        private const float purePerfectTime = 0.01f;
        private const float perfectTime = 0.05f;
        private const float greatTime = 0.1f;
        private const float goodTime = 0.15f;
        public const float badTime = 0.20f;
        #endregion

        public static Judges JudgeNotes(float time)
        {
            time = Mathf.Abs(time);
            if (time <= purePerfectTime)
            {
                return Judges.PurePerfect;
            }
            else if (time <= perfectTime)
            {
                return Judges.Perfect;
            }
            else if (time <= greatTime)
            {
                return Judges.Great;
            }
            else if (time <= goodTime)
            {
                return Judges.Good;
            }
            else if (time <= badTime)
            {
                return Judges.Bad;
            }
            return Judges.None;
        }
    }
}
