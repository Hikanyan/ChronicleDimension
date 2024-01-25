using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class NotesManager : MonoBehaviour
{
    [Header("NotesSE")] [SerializeField] public string perfectSoundName;
    [SerializeField] public string greatSoundName;
    [SerializeField] public string goodSoundName;
    [SerializeField] public string missSoundName;
    [SerializeField] public string noneTapSoundName;
    [SerializeField] public string holdSoundName;

    bool _autoMode;
    float _judgeOffset;

    private NotesGenerator _notesGenerator;
    private NotesController _notesController;
    private RhythmGameScore _rhythmGameScore;
    private TimerManager _timer;

    public List<Notes>[] blockNotes = new List<Notes>[4];

    private void Start()
    {
    }

    /// <summary> セーブデータをロードする </summary>
    void SaveLoadSettings()
    {
        // PlayerSettings settings = SaveLoad.LoadSettings<PlayerSettings>();
        // _autoMode = RhythmGameManager.Instance.AutoMode;
        // _judgeOffset = settings.judgeOffset;
    }


    void SetBlockNotes(List<Notes>[] blockNotesist)
    {
        blockNotes = blockNotesist;
        int notesNum = 0;
        foreach (var blockNoteCount in blockNotes)
        {
            foreach (var notesCount in blockNoteCount)
            {
                notesNum++;
                if (notesCount._type == Notes.NotesType.TapNote)
                {
                    notesNum++;
                }
            }
        }
    }

    private void Update()
    {
        if (_autoMode) AutoMode();
    }

    void AutoMode()
    {
        for (int i = 0; i < 4; i++)
        {
            if (blockNotes[i].Count > 0)
            {
                if (blockNotes[i][0]._type == Notes.NotesType.TapNote)
                {
                    if (blockNotes[i][0]._time - _judgeOffset <= _timer.RealTime.Value)
                    {
                    }
                }
                else if (blockNotes[i][0]._type == Notes.NotesType.HoldNote)
                {
                    if (blockNotes[i][0]._time - _judgeOffset <= _timer.RealTime.Value)
                    {
                    }
                }
            }
        }
    }

    /// <summary> ノーツの判定を行う </summary>
    void NotesJudge(Notes notes)
    {
    }

    /// <summary> 入力の判定を行う </summary>
    void InputJudge()
    {
    }

    /// <summary> ノーツの判定を評価する </summary>
    void ApplyJudge(Judges judge)
    {
        _rhythmGameScore.EvaluateNote(judge);
    }
}