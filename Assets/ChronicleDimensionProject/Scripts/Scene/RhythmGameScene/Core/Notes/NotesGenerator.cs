using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using System.IO;
using ChronicleDimensionProject.Common;

namespace ChronicleDimensionProject.RhythmGame.Notes
{
    public class NotesGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _tapNotesObject; // ノーツのプレハブを入れる
        [SerializeField] private GameObject _holdNotesObject; // ロングノーツのプレハブを入れる
        [SerializeField] private GameObject _nebulaNotesObject;
        [SerializeField] private GameObject _flareNotesObject;
        [SerializeField] private float _notesSpeed; // ノーツのスピード
        [SerializeField] private float _blockHeight; // ブロックの奥行き、ノーツが表示される奥行きz
        
        private NotesManager _notesManager;
        private NotesController _notesController;
        private NoteCollection _noteCollection;

        // ノーツを保持するリスト
        private readonly List<List<Notes>> _activeNotes = new List<List<Notes>> { new(), new(), new(), new() };

        // SUSファイルを読み込み、ノーツデータを生成する
        public async UniTask StartLoad(AssetReferenceT<TextAsset> susReference)
        {
            string dataStr = "";

#if UNITY_IOS && UNITY_EDITOR
            string path = AssetDatabase.GetAssetPath(susReference.Asset);
            dataStr = File.ReadAllText(path);
#else
            TextAsset data = await Addressables.LoadAssetAsync<TextAsset>(susReference);
            dataStr = data.text;
#endif
            _noteCollection = JsonUtility.FromJson<NoteCollection>(dataStr);

            // ノーツデータを取得
            ProcessTapNotes();
            ProcessHoldNotes();

            // 時間順にノーツをソート
            _noteCollection.SortNotesByTime();

            // ノーツをNotesManagerにセット
            _notesManager.SetBlockNotes(_activeNotes.ToArray());
            _notesController.SetData(_blockHeight, _notesSpeed);

            // 非表示にして初期化完了を通知
            gameObject.SetActive(false);
            Debug.Log("Notes Generation Complete");
        }

        // タップノーツのデータを処理する
        private void ProcessTapNotes()
        {
            foreach (var tapNote in _noteCollection.TapNotes)
            {
                switch (tapNote._type)
                {
                    case 1:
                        GenerateNoteInstance(NotesType.Tap, tapNote._block, tapNote._time);
                        break;
                    case 2:
                        GenerateNoteInstance(NotesType.ExTap, tapNote._block, tapNote._time);
                        break;
                    case 3:
                        GenerateNoteInstance(NotesType.Damage, tapNote._block, tapNote._time);
                        break;
                    case 4:
                        GenerateNoteInstance(NotesType.Slide, tapNote._block, tapNote._time);
                        break;
                    case 5:
                        GenerateNoteInstance(NotesType.Flick, tapNote._block, tapNote._time);
                        break;
                    default:
                        break;
                }
            }
        }

        // ホールドノーツのデータを処理する
        private void ProcessHoldNotes()
        {
            foreach (var holdNote in _noteCollection.HoldNotes)
            {
                GenerateHoldNoteInstance(holdNote._block, holdNote._time[0], holdNote._time[1]);
            }
        }

        // ノートのインスタンスを生成する
        private void GenerateNoteInstance(NotesType type, int block, float time)
        {
            GameObject notesObject = GetNotePrefab(type);
            if (notesObject == null) return;

            GameObject notesInstance = Instantiate(notesObject, new Vector3(block - 1.5f, 0.55f, _blockHeight + 1), notesObject.transform.rotation);
            notesInstance.transform.SetParent(_notesManager.transform, false);

            Notes notes = notesInstance.GetComponent<Notes>();
            notes.Track = block;
            notes.NotesType = type;
            notes.SpawnTime = time;

            _activeNotes[block].Add(notes);
        }

        // ホールドノートのインスタンスを生成する
        private void GenerateHoldNoteInstance(int block, float startTime, float endTime)
        {
            GameObject notesObject = _holdNotesObject;
            if (notesObject == null) return;

            GameObject notesInstance = Instantiate(notesObject, new Vector3(block - 1.5f, 0.55f, _blockHeight + 1), notesObject.transform.rotation);
            notesInstance.transform.SetParent(_notesManager.transform, false);

            Notes notes = notesInstance.GetComponent<Notes>();
            notes.Track = block;
            notes.NotesType = NotesType.Hold;
            notes.SpawnTime = startTime;
            notes._endTime = endTime;

            _activeNotes[block].Add(notes);
        }

        // ノーツのプレハブを取得する
        private GameObject GetNotePrefab(NotesType type)
        {
            return type switch
            {
                NotesType.Tap => _tapNotesObject,
                NotesType.Hold => _holdNotesObject,
                NotesType.ExTap => _nebulaNotesObject,
                NotesType.Damage => _flareNotesObject,
                _ => null,
            };
        }
    }
}
