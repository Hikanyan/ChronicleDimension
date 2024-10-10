using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ChronicleDimensionProject.RhythmGame.Notes
{
    [Serializable]
    public class NoteCollection
    {
        public List<TapNotesInput> TapNotes { get; private set; } = new List<TapNotesInput>();
        public List<HoldNotesInput> HoldNotes { get; private set; } = new List<HoldNotesInput>();

        public void AddTapNote(TapNotesInput tapNote)
        {
            TapNotes.Add(tapNote);
        }

        public void AddHoldNote(HoldNotesInput holdNote)
        {
            HoldNotes.Add(holdNote);
        }

        public IEnumerable<INoteInput> GetAllNotes()
        {
            return TapNotes.Cast<INoteInput>().Concat(HoldNotes);
        }

        public void Quantize(float resolution)
        {
            foreach (var note in GetAllNotes())
            {
                note.Quantize(resolution);
            }
        }

        public void SortNotesByTime()
        {
            TapNotes = TapNotes.OrderBy(n => n._time).ToList();
            HoldNotes = HoldNotes.OrderBy(n => n._time[0]).ToList();
        }
    }

    public interface INoteInput
    {
        void Quantize(float resolution);
    }

    [Serializable]
    public class TapNotesInput : INoteInput
    {
        public int _type;
        public float _time;
        public int _block;

        public void Quantize(float resolution)
        {
            _time = Mathf.Round(_time / resolution) * resolution;
        }
    }

    [Serializable]
    public class HoldNotesInput : INoteInput
    {
        public int _type;
        public float[] _time;
        public int _block;

        public void Quantize(float resolution)
        {
            for (int i = 0; i < _time.Length; i++)
            {
                _time[i] = Mathf.Round(_time[i] / resolution) * resolution;
            }
        }
    }
}