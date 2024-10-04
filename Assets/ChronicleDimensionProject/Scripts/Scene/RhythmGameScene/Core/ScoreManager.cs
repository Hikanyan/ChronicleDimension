using ChronicleDimensionProject.Common;
using ChronicleDimensionProject.RhythmGame.JudgeData;
using TMPro;
using UnityEngine;

namespace ChronicleDimensionProject.RhythmGameScene
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        [Header("Score")] [SerializeField] TextMeshProUGUI _scoreText;

        [Header("ResultText")] [SerializeField]
        TextMeshProUGUI _resultRankText;

        [SerializeField] TextMeshProUGUI _resultScoreText;
        [SerializeField] TextMeshProUGUI _resultJudgeText;
        [Header("ComboText")] [SerializeField] TextMeshProUGUI _comboText;

        public int Sum { get; } = 0;
        public int Combo { get; set; } = 0;
        private int _maxCombo = 0;

        public JudgeScores _judgeScores;

        public static ScoreManager Instance;
        public int MaxScore;
        public int SingleScore;
        public int Score = 0;
        public int NotesNum;
        public float clearPercent;

        protected override void OnAwake()
        {
            _comboText.gameObject.SetActive(false);

            _judgeScores.PurePerfect = 0;
            _judgeScores.PurePerfect = 0;
            _judgeScores.Great = 0;
            _judgeScores.Good = 0;
            _judgeScores.Bad = 0;
            _judgeScores.Miss = 0;
            _scoreText.text = $"0";
        }

        public void SetMaxScore()
        {
            Debug.Log(NotesNum);
            //if (NotesNum == 0) return;
            MaxScore = 10000000 + NotesNum;
            SingleScore = MaxScore / NotesNum;
        }

        public void AddScore(Judges judge)
        {
            float scorePercent = 1.0f;
            switch (judge)
            {
                case Judges.PurePerfect:
                    scorePercent = 1.0f;
                    break;
                case Judges.Perfect:
                    scorePercent = 0.9f;
                    break;
                case Judges.Great:
                    scorePercent = 0.8f;
                    break;
                case Judges.Good:
                    scorePercent = 0.5f;
                    break;
                case Judges.Bad:
                    scorePercent = 0.3f;
                    break;
                case Judges.Miss:
                    scorePercent = 0.0f;
                    break;
                default:
                    break;
            }

            Score += Mathf.FloorToInt(scorePercent * SingleScore);
            _scoreText.text = $"{Score}";
        }

        string GetRank()
        {
            clearPercent = (float)Score / MaxScore;
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
                >= 0.92f => "BB",
                >= 0.91f => "B",
                >= 0.90f => "CCC",
                >= 0.80f => "CC",
                >= 0.70f => "C",
                _ => "D"
            };
        }

        public void SetCombo(bool isCombo)
        {
            if (isCombo)
            {
                Combo++;
                if (Combo < 2) return;

                _comboText.text = $"Combo {Combo}";
                if (Combo == 2)
                {
                    _comboText.gameObject.SetActive(true);
                }

                if (Combo > _maxCombo)
                {
                    _maxCombo = Combo;
                }
            }
            else
            {
                Combo = 0;
                _comboText.gameObject.SetActive(false);
            }
        }

        public ResultData GetResultData()
        {
            ResultData resultData = new ResultData
            {
                Rank = GetRank(),
                ClearPercent = clearPercent,
                Score = Score,
                JudgeScores = _judgeScores,
                MaxCombo = _maxCombo
            };
            return resultData;
        }
    }
}