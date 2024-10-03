using System.Collections.Generic;
using ChronicleDimensionProject.Common;
using ChronicleDimensionProject.RhythmGame.JudgeData;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace ChronicleDimensionProject.RhythmGame.Notes
{
    public class NotesManager : MonoBehaviour
    {
        public static NotesManager instance = null;

        [SerializeField] public AudioClip _perfectSound;
        [SerializeField] public AudioClip _greatSound;
        [SerializeField] public AudioClip _goodSound;
        [SerializeField] public AudioClip _missSound;
        [SerializeField] public AudioClip _noneTapSound;
        [SerializeField] public AudioClip _holdSound;

        [SerializeField] Text _text;

        private bool _autoMode = false;

        float _judgeOffset = 0.0f;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            if (AutoMode.Instance != null)
            {
                _autoMode = AutoMode.Instance._autoMode;
            }

            PlayerData.PlayerSettings settings = SaveLoad.LoadSettings<PlayerData.PlayerSettings>();
            _judgeOffset = settings.RhythmGamePlayerData.JudgeOffset;
        }

        public List<Notes>[] blockNotes = new List<Notes>[4];

        public void SetBlockNotes(List<Notes>[] blockNotesist)
        {
            blockNotes = blockNotesist;
            int notesNum = 0;
            foreach (var blockNoteCount in blockNotes)
            {
                foreach (var notesCount in blockNoteCount)
                {
                    notesNum++;
                    if (notesCount._type == NotesType.Meteor)
                    {
                        notesNum++;
                    }
                }
            }

            ScoreManager.Instance.NotesNum = notesNum;
        }


        private void Update()
        {
            if (!_autoMode) return;

            for (int i = 0; i < 4; i++)
            {
                if (blockNotes[i].Count < 1) return;
                if ((blockNotes[i][0]._type != NotesType.Flare) &&
                    Timer.instance.realTime - blockNotes[i][0]._time >= 0.0f && !blockNotes[i][0]._holding)
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
                    AudioManager.instance.PlayLoopSound(_holdSound, Audio.SE, notes._block + 10);
                }

                if (release && notes._type == NotesType.Meteor && notes._holding) //ホールド最後で離したとき
                {
                    ParticleManager.Instance.HoldEffect(notes._block, false);
                    NotesManager.instance.blockNotes[notes._block].RemoveAt(0);
                    AudioManager.instance.StopLoopSound(notes._block + 10);
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
                    AudioManager.instance.StopLoopSound(notes._block + 10);
                }
                else //空タップ
                {
                    if (_noneTapSound != null)
                    {
                        AudioManager.instance.PlaySound(_noneTapSound, Audio.SE);
                    }
                }
            }
        }

        public void ApplyJudge(Judges judge, int block, bool showParticle = true)
        {
            ScoreManager.Instance.AddScore(judge);

            if (Judges.PurePerfect == judge)
            {
                ScoreManager.Instance.Combo(true);
                ScoreManager.Instance._judgeScores.PurePerfect++;
            }
            else if (Judges.Perfect == judge)
            {
                ScoreManager.Instance.Combo(true);
                ScoreManager.Instance._judgeScores.Perfect++;
            }
            else if (Judges.Great == judge)
            {
                ScoreManager.Instance.Combo(true);
                ScoreManager.Instance._judgeScores.Great++;
            }
            else if (Judges.Good == judge)
            {
                ScoreManager.Instance.Combo(false);
                ScoreManager.Instance._judgeScores.Good++;
            }
            else if (Judges.Bad == judge)
            {
                ScoreManager.Instance.Combo(false);
                ScoreManager.Instance._judgeScores.Bad++;
            }
            else if (Judges.Miss == judge)
            {
                ScoreManager.Instance.Combo(false);
                ScoreManager.Instance._judgeScores.Miss++;
            }

            if (showParticle)
            {
                ParticleManager.Instance.JudgeEffect(judge, block);


                AudioClip selectSound = null;
                switch (judge)
                {
                    case Judges.PurePerfect:
                    case Judges.Perfect:
                        selectSound = _perfectSound;
                        break;
                    case Judges.Great:
                        selectSound = _greatSound;
                        break;
                    case Judges.Good:
                        selectSound = _goodSound;
                        break;
                    case Judges.Bad:
                    case Judges.Miss:
                        selectSound = _missSound;
                        break;
                    case Judges.None:
                        break;
                }


                if (selectSound != null)
                {
                    AudioManager.instance.PlaySound(selectSound, Audio.SE);
                }
            }
        }
    }
}