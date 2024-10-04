using System.Collections.Generic;
using System.Linq;
using ChronicleDimensionProject.Common;
using ChronicleDimensionProject.RhythmGame.JudgeData;
using ChronicleDimensionProject.RhythmGameScene;
using UnityEngine;

namespace ChronicleDimensionProject.RhythmGame.Notes
{
    public class NotesController : Singleton<NotesController>
    {
        private RhythmGameManager _rhythmGameManager;
        private RhythmGameTimer _rhythmGameTimer;
        private NotesManager _notesManager;
        private float _blockHeight;
        private float _notesSpeed;

        public void SetData(float blockHeight, float notesSpeed)
        {
            _blockHeight = blockHeight;
            _notesSpeed = notesSpeed;
        }

        private readonly List<int> _removeNotesByLaneIndex = new();

        void Update()
        {
            if (!_rhythmGameTimer._isRunning) return;
            MoveNotes();
            CheckNotesIsNothing();
        }

        void CheckNotesIsNothing()
        {
            if (_notesManager.blockNotes.Any(lane =>
                    lane.Count > 0)) return; // ノーツが残っている場合、何もしない

            _rhythmGameManager.GameEnd(); // 全てのノーツが処理されたらゲーム終了
        }

        private void MoveNotes()
        {
            for (int laneIndex = 0; laneIndex < 4; laneIndex++)
            {
                foreach (Notes note in _notesManager.blockNotes[laneIndex])
                {
                    ControlNotePosition(laneIndex, note);
                }
            }

            // ノーツが処理された場合にリストから削除
            if (_removeNotesByLaneIndex.Count <= 0) return;
            foreach (var index in _removeNotesByLaneIndex.Where(index =>
                         _notesManager.blockNotes[index].Count > 0))
            {
                _notesManager.blockNotes[index].RemoveAt(0);
            }

            _removeNotesByLaneIndex.Clear();
        }

        private void ControlNotePosition(int laneIndex, Notes note)
        {
            // ノーツの時間（ノーツが流れてくるタイミング）に基づく位置計算
            float timeSinceStart = _rhythmGameTimer.RealTime - note.SpawnTime;
            float position = _blockHeight - timeSinceStart / _blockHeight * _blockHeight * _notesSpeed;

            // ノーツが可視範囲に入った時に表示
            if (!note.IsVisible && position <= _blockHeight)
            {
                note.SetVisible(true);
            }

            // ノーツが見えている場合、移動を続ける
            if (note.IsVisible)
            {
                note.MoveNote(_notesSpeed);
            }

            // ノーツが判定エリアを過ぎた場合の処理
            if (timeSinceStart > JudgeTime.BadTime && !note.IsHit)
            {
                _notesManager.ApplyJudge(Judges.Miss, note.Track);
                note.SetVisible(false);
                note.IsHit = true;
                _removeNotesByLaneIndex.Add(laneIndex);
            }
        }
    }
}