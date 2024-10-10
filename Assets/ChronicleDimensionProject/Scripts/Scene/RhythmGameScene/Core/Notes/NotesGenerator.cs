using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using System.IO;
using ChronicleDimensionProject.Common;
using UnityEditor;

namespace ChronicleDimensionProject.RhythmGame.Notes
{
    public class NotesGenerator : MonoBehaviour
    {
        public List<int> _laneNum = new(); //何番目のレーンにノーツが落ちてくるか
        public List<int> _noteType = new(); //Notesの種類
        public List<float> _notesTime = new(); //ノーツが判定線と重なる時間
        public List<GameObject> _notesObject = new(); //GameObject


        //[SerializeField] private string _sonfName;//曲名を入れる関数を作成する。保存したJsonの名前を入れる
        [SerializeField] private GameObject _tapNotesObject; //ノーツのプレハブを入れる
        [SerializeField] private GameObject _holdNotesObject; //ロングノーツのプレハブを入れる
        [SerializeField] private GameObject _nebulaNotesObject;
        [SerializeField] private GameObject _flareNotesObject;
        [SerializeField] private float _notesSeed; //ノーツのスピード
        [SerializeField] private float _blockHeight; //ブロックの奥行き、ノーツが表示される奥行きz
        private NotesManager _notesManager;
        private NotesController _notesController;

        //有効にされたらJsonファイルを読み込み、座標を計算して配置する
        //Updateを使わない理由はPlay中にズレないようにするため
        //Jsonを使用せず直接susファイルをしようするラッパークラスを参照するのもあり

        private NoteCollection _noteCollection;

        //[SerializeField] private List<INotes>[] _notesIFList = { null, null, null, null };
        private readonly List<List<Notes>> _activeNotes = new List<List<Notes>>() { new(), new(), new(), new() };


        public async UniTask StartLoad(AssetReferenceT<TextAsset> susReference)
        {
            string dataStr = "";

#if UNITY_IOS && UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(jsonReference.Asset);
        dataStr = File.ReadAllText(path);
#else
            TextAsset data = await Addressables.LoadAssetAsync<TextAsset>(susReference);
            dataStr = data.text;
#endif

            _noteCollection = JsonUtility.FromJson<Data>(dataStr); //

            int sumTapNotes = _noteCollection._tapNotes.Length;
            int sumHoldNotes = _noteCollection._holdNotes.Length;
            for (int i = 0; i < sumTapNotes; i++) //ノーツの位置を一個ずつ配置していく
            {
                switch (_noteCollection._tapNotes[i]._type)
                {
                    case 1:
                        GetData(NotesType.Tap, i);
                        break;
                    case 2:
                        GetData(NotesType.ExTap, i);
                        break;
                    case 3:
                        GetData(NotesType.Damage, i);
                        break;
                    case 4:
                        GetData(NotesType.Slide, i);
                        break;
                    case 5:
                        GetData(NotesType.Flick, i);
                        break;

                    default:
                        break;
                }
            }

            for (int i = 0; i < sumHoldNotes; i++) //ノーツの位置を一個ずつ配置していく
            {
                GetData(NotesType.Hold, i);
            }

            //時間が早い時間順にソートする
            List<Notes>[] SortNotes = new List<Notes>[] { new(), new(), new(), new() };
            for (int i = 0; i < _activeNotes.Count(); i++)
            {
                var sortResult = _activeNotes[i].OrderBy(d => d._time); //time昇順にソート
                foreach (var order in sortResult)
                {
                    SortNotes[i].Add(order);
                }
            }

            _notesManager.SetBlockNotes(SortNotes);
            _notesController.SetData(_blockHeight, _notesSeed);
            gameObject.SetActive(false);
            Debug.Log("Notes Generate Complate");

            await Task.Run(() => { }).ConfigureAwait(false);
        }

        private void GetData(NotesType type, int i)
        {
            GameObject notesObject;

            int _track = 0;

            switch (type)
            {
                case NotesType.Tap:
                    notesObject = _tapNotesObject;
                    _track = _noteCollection._tapNotes[i]._block;
                    break;
                case NotesType.Hold:
                    notesObject = _holdNotesObject;
                    _track = _noteCollection._holdNotes[i]._block;
                    break;
                case NotesType.ExTap:
                    notesObject = _nebulaNotesObject;
                    _track = _noteCollection._tapNotes[i]._block;
                    break;
                case NotesType.Damage:
                    notesObject = _flareNotesObject;
                    _track = _noteCollection._tapNotes[i]._block;
                    break;
                default:
                    notesObject = null;
                    break;
            }

            if (notesObject == null) return;
            GameObject notesInstance = Instantiate(notesObject, new Vector3(_track - 1.5f, 0.55f, _blockHeight + 1),
                notesObject.transform.rotation);
            notesInstance.transform.SetParent(_notesManager.transform, false);
            Notes notes = notesInstance.GetComponent<Notes>();
            notes.Track = _track;
            notes.NotesType = type;
            switch (type)
            {
                case NotesType.Star:
                    notes._time = _noteCollection._tapNotes[i]._time;
                    break;
                case NotesType.Meteor:
                    notes._time = _noteCollection._holdNotes[i]._time[0];
                    notes._endTime = _noteCollection._holdNotes[i]._time[1];
                    break;
                case NotesType.Nebula:
                    notes._time = _noteCollection._tapNotes[i]._time; //DEBUG
                    break;
                case NotesType.Flare:
                    notes._time = _noteCollection._tapNotes[i]._time; //DEBUG
                    break;
                default:
                    break;
            }

            _activeNotes[notes._block].Add(notes);
            return;
        }
    }

/*
 * 参考資料
 *
 *
 * https://qiita.com/irigoma77/items/ee15b3e748596aa6d086
 * https://www.youtube.com/watch?v=WWeyn4TI0lI&t=327s
 * https://www.youtube.com/watch?v=TnKnwLIiY_8
 *
 *
 *
 */
}