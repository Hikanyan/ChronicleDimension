using System;
using System.Collections.Generic;
using System.Linq;
using ChronicleDimensionProject.Common;
using ChronicleDimensionProject.RhythmGame.JudgeData;
using ChronicleDimensionProject.RhythmGameScene;
using ChronicleDimention.CueSheet_SE;
using HikanyanLaboratory.Audio;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace ChronicleDimensionProject.RhythmGame.Notes
{
    public class NotesManager : Singleton<NotesController>
    {
        [SerializeField] public Cue _perfectSound;
        [SerializeField] public Cue _greatSound;
        [SerializeField] public Cue _goodSound;
        [SerializeField] public Cue _missSound;
        [SerializeField] public Cue _noneTapSound;
        [SerializeField] public Cue _holdSound;

        [SerializeField] Text _text;

        private bool _autoMode = false;
        private float _judgeOffset = 0.0f;
        private ScoreManager _scoreManager;
        private CriAudioManager _criAudioManager;
        private RhythmGameTimer _rhythmGameTimer;
        private ParticleManager _particleManager;
        private PlayerData.PlayerSettings _playerSettings;


        private void Start()
        {
            _playerSettings = SaveLoad.LoadSettings<PlayerData.PlayerSettings>();
            // 各レーンのノーツリストを初期化
            for (int i = 0; i < blockNotes.Length; i++)
            {
                blockNotes[i] = new List<Notes>();
            }

            if (AutoMode.Instance != null)
            {
                _autoMode = AutoMode.Instance._autoMode;
            }

            _judgeOffset = _playerSettings.RhythmGamePlayerData.JudgeOffset;
        }

        public List<Notes>[] blockNotes = new List<Notes>[4];

        public void SetBlockNotes(List<Notes>[] notes)
        {
            blockNotes = notes;
            int notesNum = 0;
            // ノーツの総数をカウントしてScoreManagerに渡す
            int totalNotes = blockNotes.Sum(lane => lane.Count);

            ScoreManager.Instance.NotesNum = totalNotes;
        }


        private void Update()
        {
            if (!_autoMode) return;

            for (int i = 0; i < 4; i++)
            {
                if (blockNotes[i].Count < 1) return;
                if (blockNotes[i][0].NotesType != NotesType.Tap &&
                    _rhythmGameTimer.RealTime - blockNotes[i][0].SpawnTime >= 0.0f && !blockNotes[i][0].IsHolding)
                {
                    BlockPress(i);
                }
            }
        }

        public void BlockPress(int block)
        {
            if (blockNotes[block].Count < 1) return;
            if (blockNotes[block][0].NotesType == NotesType.Damage) return;
            NotesJudge(blockNotes[block][0], false);
        }

        public void NotesJudge(Notes notes, bool release)
        {
            float judgetime = _rhythmGameTimer.RealTime - notes.SpawnTime;

            if (release && notes.NotesType == NotesType.Hold)
            {
                judgetime = _rhythmGameTimer.RealTime - notes.Duration;
            }

            judgetime += _judgeOffset;

            Judges judge = JudgeTime.JudgeNotes(judgetime);
            if (judge != Judges.None)
            {
                ApplyJudge(judge, notes.Track);

                if (notes.NotesType == NotesType.Tap && !release)
                {
                    notes.SetVisible(false);
                    blockNotes[notes.Track].RemoveAt(0);
                }
            }
        }

        public void ApplyJudge(Judges judge, int block, bool showParticle = true)
        {
            ScoreManager.Instance.AddScore(judge);

            switch (judge)
            {
                case Judges.PurePerfect:
                    _scoreManager.SetCombo(true);
                    _scoreManager._judgeScores.PurePerfect++;
                    break;
                case Judges.Perfect:
                    _scoreManager.SetCombo(true);
                    _scoreManager._judgeScores.Perfect++;
                    break;
                case Judges.Great:
                    _scoreManager.SetCombo(true);
                    _scoreManager._judgeScores.Great++;
                    break;
                case Judges.Good:
                    _scoreManager.SetCombo(false);
                    _scoreManager._judgeScores.Good++;
                    break;
                case Judges.Bad:
                    _scoreManager.SetCombo(false);
                    _scoreManager._judgeScores.Bad++;
                    break;
                case Judges.Miss:
                    _scoreManager.SetCombo(false);
                    _scoreManager._judgeScores.Miss++;
                    break;
                case Judges.Auto:
                    _scoreManager.SetCombo(true);
                    _scoreManager._judgeScores.Auto++;
                    break;
                case Judges.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(judge), judge, null);
            }

            // パーティクルエフェクトの再生
            if (showParticle)
            {
                _particleManager.JudgeEffect(judge, block);
            }

            // サウンドの再生
            PlayJudgeSound(judge);
        }

        // 判定に応じたサウンドを再生
        private void PlayJudgeSound(Judges judge)
        {
            Cue sound = judge switch
            {
                Judges.PurePerfect or Judges.Perfect => _perfectSound,
                Judges.Great => _greatSound,
                Judges.Good => _goodSound,
                Judges.Bad or Judges.Miss => _missSound,
                _ => default
            };

            if (sound != default)
            {
                _criAudioManager.Play(CriAudioType.CueSheet_SE, sound);
            }
        }
    }
}