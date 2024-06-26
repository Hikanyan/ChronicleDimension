﻿using System.Collections.Generic;
using ChronicleDimensionProject.Common;
using UnityEngine;

public class NotesController : AbstractSingletonMonoBehaviour<NotesController>
{
    protected override bool UseDontDestroyOnLoad => false;
    float _blockHeight;
    float _notesSpeed;
    List<int> _removeNotesByLaneIndex = new();

    public void SetData(float blockHeight, float notesSpeed)
    {
        _blockHeight = blockHeight;
        _notesSpeed = notesSpeed;
    }

    void Update()
    {
        if (RhythmGameManager.Instance.timerManager.IsRunning.Value)
        {
            MoveNotes();
            CheckNotesIsNothing();
        }
    }

    void MoveNotes()
    {
    }

    private void NotesControl(int i, Notes notes)
    {
    }

    void CheckNotesIsNothing()
    {
        RhythmGameManager.Instance.GameEnd();
    }
}