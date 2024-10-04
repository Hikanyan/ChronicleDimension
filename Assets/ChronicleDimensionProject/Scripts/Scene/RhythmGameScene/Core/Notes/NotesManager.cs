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
        public static NotesManager instance = null;

        [SerializeField] public Cue _perfectSound;
        [SerializeField] public Cue _greatSound;
        [SerializeField] public Cue _goodSound;
        [SerializeField] public Cue _missSound;
        [SerializeField] public Cue _noneTapSound;
        [SerializeField] public Cue _holdSound;

        [SerializeField] Text _text;

        private bool _autoMode = false;
        float _judgeOffset = 0.0f;
        ScoreManager _scoreManager;
        CriAudioManager _criAudioManager;
        RhythmGameTimer _rhythmGameTimer;

        private void Start()
        {
            // 各レーンのノーツリストを初期化
            for (int i = 0; i < blockNotes.Length; i++)
            {
                blockNotes[i] = new List<Notes>();
            }

            if (AutoMode.Instance != null)
            {
                _autoMode = AutoMode.Instance._autoMode;
            }

            PlayerData.PlayerSettings settings = SaveLoad.LoadSettings<PlayerData.PlayerSettings>();
            _judgeOffset = settings.RhythmGamePlayerData.JudgeOffset;
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
                if ((blockNotes[i][0]._type != NotesType.Flare) &&
                    _rhythmGameTimer.RealTime - blockNotes[i][0]._time >= 0.0f && !blockNotes[i][0]._holding)
                {
                    BlockPress(i);
                    if (blockNotes[i][0]._type == NotesType.Nebula)
                    {
                        BlockALLPress();
                    }
                }

                if (blockNotes[i][0]._type == NotesType.Meteor &&
                    Timer.instance.realTime - blockNotes[i][0]._endTime >= 0.0f)
                {
                    BlockRelease(i);
                }
            }
        }

        public void BlockALLPress()
        {
            float fastTime = 1000.0f;
            List<int> fastBlocks = new();
            for (int i = 0; i < 4; i++)
            {
                bool find = false;
                float curTime = 1000;
                foreach (Notes note in blockNotes[i])
                {
                    if (note._type == NotesType.Nebula)
                    {
                        find = true;
                        curTime = note._time;
                        break;
                    }
                }

                if (!find) continue;
                if (blockNotes[i].Count < 1) continue;

                if (fastTime == curTime)
                {
                    fastBlocks.Add(i);
                }
                else if (fastTime > curTime)
                {
                    fastTime = curTime;
                    fastBlocks.Clear();
                    fastBlocks.Add(i);
                }
            }

            if (fastBlocks.Count < 1) return;
            //Debug.Log($"{blockNotes[0][0]._time} {blockNotes[1][0]._time} {blockNotes[2][0]._time} {blockNotes[3][0]._time} = {fastBlock}");
            foreach (int i in fastBlocks)
            {
                NotesJudge(blockNotes[i][0], false);
            }
        }

        public void BlockPress(int block)
        {
            if (blockNotes[block].Count < 1) return;
            if (blockNotes[block][0]._type == NotesType.Nebula) return;
            NotesJudge(blockNotes[block][0], false);
        }

        public void BlockRelease(int block)
        {
            if (blockNotes[block].Count <= 0) return;
            if (blockNotes[block][0]._type == NotesType.Nebula) return;
            if (blockNotes[block][0]._type != NotesType.Meteor) return;

            NotesJudge(blockNotes[block][0], true);
        }

        public void NotesJudge(Notes notes, bool release) //@インターフェースにしましょう
        {
            float judgetime = Timer.instance.realTime - notes._time;

            if (release && notes._type == NotesType.Meteor)
            {
                judgetime = Timer.instance.realTime - notes._endTime;
            }

            //+で早く叩いても反応, -で遅く叩いても反応
            judgetime += _judgeOffset; //判定調整


            if (JudgeTime.JudgeNotes(judgetime) != Judges.None)
            {
                if (notes._type != NotesType.Flare)
                {
                    ApplyJudge(JudgeTime.JudgeNotes(judgetime), notes._block);
                }


                if ((notes._type == NotesType.Star || notes._type == NotesType.Nebula) && !release) //シングルノーツを押したとき
                {
                    notes.SetVisible(false);
                    NotesManager.instance.blockNotes[notes._block].RemoveAt(0);
                }

                if (notes._type == NotesType.Flare && !release) //ダメージノーツを押したとき
                {
                    ApplyJudge(Judges.Miss, notes._block);
                    NotesManager.instance.blockNotes[notes._block].RemoveAt(0);
                    notes.SetVisible(false);
                }

                if (notes._type == NotesType.Meteor && !release) //ホールドの最初押したとき
                {
                    notes._holding = true;
                    ParticleManager.Instance.HoldEffect(notes._block, true);
                    _criAudioManager.PlayLoopSound(_holdSound, Audio.SE, notes._block + 10);
                }

                if (release && notes._type == NotesType.Meteor && notes._holding) //ホールド最後で離したとき
                {
                    ParticleManager.Instance.HoldEffect(notes._block, false);
                    NotesManager.instance.blockNotes[notes._block].RemoveAt(0);
                    _criAudioManager.StopLoopSound(notes._block + 10);
                    notes.SetVisible(false);
                }
            }
            else
            {
                if (release && notes._type == NotesType.Meteor && notes._holding) //ホールド途中で離したとき
                {
                    ApplyJudge(Judges.Miss, notes._block);
                    NotesManager.instance.blockNotes[notes._block].RemoveAt(0);
                    notes.SetVisible(false);
                    ParticleManager.Instance.HoldEffect(notes._block, false);
                    _criAudioManager.StopLoopSound(notes._block + 10);
                }
                else //空タップ
                {
                    if (_noneTapSound != null)
                    {
                        _criAudioManager.PlaySound(_noneTapSound, Audio.SE);
                    }
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
                    ScoreManager.Instance._judgeScores.PurePerfect++;
                    break;
                case Judges.Perfect:
                    _scoreManager.SetCombo(true);
                    ScoreManager.Instance._judgeScores.Perfect++;
                    break;
                case Judges.Great:
                    _scoreManager.SetCombo(true);
                    ScoreManager.Instance._judgeScores.Great++;
                    break;
                case Judges.Good:
                    _scoreManager.SetCombo(false);
                    ScoreManager.Instance._judgeScores.Good++;
                    break;
                case Judges.Bad:
                    _scoreManager.SetCombo(false);
                    ScoreManager.Instance._judgeScores.Bad++;
                    break;
                case Judges.Miss:
                    _scoreManager.SetCombo(false);
                    ScoreManager.Instance._judgeScores.Miss++;
                    break;
            }

            // パーティクルエフェクトの再生
            if (showParticle)
            {
                ParticleManager.Instance.JudgeEffect(judge, block);
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
