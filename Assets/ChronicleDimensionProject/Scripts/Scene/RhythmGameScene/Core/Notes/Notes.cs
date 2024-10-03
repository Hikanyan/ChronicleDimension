﻿using UnityEngine;

namespace ChronicleDimensionProject.RhythmGame.Notes
{
    public enum NotesType
    {
        None,
        NormalNotes,
        LongNotes,
        DamageNotes,
    }

    public class Notes : MonoBehaviour
    {
        [SerializeField] private NotesType _noteType = NotesType.None; // ノーツの種類
        [SerializeField] private float _spawnTime;                     // ノーツが出現するタイミング
        [SerializeField] private float _duration;                      // ノーツの持続時間（ロングノーツ用）
        [SerializeField] private bool _isVisible;                      // ノーツが表示されているかどうか
        [SerializeField] private bool _isHit;                          // ノーツがすでにヒットされたかどうか

        public NotesType NoteType => _noteType;
        public float SpawnTime => _spawnTime;
        public float Duration => _duration;
        public bool IsVisible => _isVisible;
        public bool IsHit
        {
            get => _isHit;
            set => _isHit = value;
        }


        // 初期化メソッド
        public void Initialize(NotesType type, float time, float duration = 0)
        {
            _noteType = type;
            _spawnTime = time;
            this._duration = duration;
            _isVisible = false;
            _isHit = false;
        }

        // ノーツの表示状態を変更
        public void SetVisible(bool visible)
        {
            _isVisible = visible;
            gameObject.SetActive(visible); // ノーツのGameObjectを表示/非表示にする
        }

        // ノーツがヒットされたときの処理
        public void Hit()
        {
            if (!_isHit)
            {
                _isHit = true;
                // ノーツのヒット処理 (スコア追加、エフェクトなど)
                Debug.Log("Note hit!");
            }
        }

        // ノーツの動作 (ノーツが移動する場合など)
        public void MoveNote(float speed)
        {
            if (_isVisible)
            {
                // ここでノーツの移動処理を記述（例: z軸方向に移動）
                transform.Translate(Vector3.back * (speed * Time.deltaTime));
            }
        }

        private void Update()
        {
            // ノーツの可視状態や動作を管理
            if (!_isHit && _isVisible)
            {
                // ノーツの移動
                MoveNote(5.0f); // 例えば5.0fの速度で移動
            }
        }
    }
}