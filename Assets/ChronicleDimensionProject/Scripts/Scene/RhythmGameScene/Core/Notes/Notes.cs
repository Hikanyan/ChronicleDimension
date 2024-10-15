using UnityEngine;

namespace ChronicleDimensionProject.RhythmGame.Notes
{
    public enum NotesType
    {
        None,
        Tap,
        ExTap,
        Hold,
        Slide,
        Flick,
        Damage,
        Sky,
        SkyHold,
        SkySlide,
        Trace
    }

    public class Notes : MonoBehaviour
    {
        [SerializeField] private NotesType _notesType = NotesType.None; // ノーツの種類
        [SerializeField] private float _spawnTime; // ノーツが出現するタイミング
        [SerializeField] private float _duration; // ノーツの持続時間（ロングノーツ用）
        [SerializeField] private int _track; // ノーツが落ちるレーン番号（ブロック）
        [SerializeField] private bool _isVisible; // ノーツが表示されているかどうか
        [SerializeField] private bool _isHit; // ノーツがすでにヒットされたかどうか
        [SerializeField] public bool _holding; // ホールドノーツが押されているか

        public NotesType NotesType
        {
            get => _notesType;
            set => _notesType = value;
        }

        public float SpawnTime
        {
            get => _spawnTime;
            set => _spawnTime = value;
        }

        public float Duration => _duration;

        public int Track
        {
            get => _track;
            set => _track = value;
        }

        public bool IsVisible => _isVisible;

        public bool IsHit
        {
            get => _isHit;
            set => _isHit = value;
        }

        public bool IsHolding
        {
            get => _holding;
            set => _holding = value;
        }

        // 初期化メソッド
        public void Initialize(NotesType type, float time, int block, float duration = 0)
        {
            _notesType = type;
            _spawnTime = time;
            _track = block;
            _duration = duration;
            _isVisible = false;
            _isHit = false;
            _holding = false;
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
            if (_isHit) return;
            _isHit = true;
            // ノーツのヒット処理 (スコア追加、エフェクトなど)
            Debug.Log("Note hit!");
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