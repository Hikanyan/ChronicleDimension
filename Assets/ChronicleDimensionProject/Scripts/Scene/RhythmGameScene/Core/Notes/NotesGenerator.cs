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
    [Serializable]
    public class Data
    {
        public TapNotesInput[] _tapNotes;
        public HoldNotesInput[] _holdNotes;
    }

    [Serializable]
    public class TapNotesInput
    {
        public int _type;
        public float _time;
        public int _block;
    }

    [Serializable]
    public class HoldNotesInput
    {
        public int _type;
        public float[] _time;
        public int _block;
    }

    public class NotesGenerator : MonoBehaviour
    {
        public List<int> _laneNum = new(); //何番目のレーンにノーツが落ちてくるか
        public List<int> _noteType = new(); //Notesの種類
        public List<float> _notesTime = new(); //ノーツが判定線と重なる時間
        public List<GameObject> _notesObject = new(); //GameObject


        //[SerializeField] private string _sonfName;//曲名を入れる関数を作成する。保存したJsonの名前を入れる
        [SerializeField] private GameObject _starNotesObject; //ノーツのプレハブを入れる
        [SerializeField] private GameObject _meteorNotesObject; //ロングノーツのプレハブを入れる
        [SerializeField] private GameObject _nebulaNotesObject;
        [SerializeField] private GameObject _flareNotesObject;
        [SerializeField] private float _notesSeed; //ノーツのスピード
        [SerializeField] private float _blockHeight; //ブロックの奥行き、ノーツが表示される奥行きz
        private NotesManager _notesManager;
        private void Start() //オブジェクトが有効にされたとき一回だけ呼び出される
        {
            _notesManager.
            _notesSeed = setting.notesSpeed;
            //Load(_sonfName);//Load(sonfName)を呼び出し
        }

        //有効にされたらJsonファイルを読み込み、座標を計算して配置する
        //Updateを使わない理由はPlay中にズレないようにするため
        //Jsonを使用せず直接susファイルをしようするラッパークラスを参照するのもあり

        private Data inputJson;

        //[SerializeField] private List<INotes>[] _notesIFList = { null, null, null, null };
        private readonly List<List<Notes>> _activeNotes = new List<List<Notes>>() { new(), new(), new(), new() };


        public async Task StartLoad(AssetReferenceT<TextAsset> jsonReference)
        {
            string dataStr = "";

#if UNITY_IOS && UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(jsonReference.Asset);
        dataStr = File.ReadAllText(path);
#else
            TextAsset data = await Addressables.LoadAssetAsync<TextAsset>(jsonReference);
            dataStr = data.text;
#endif

            inputJson = JsonUtility.FromJson<Data>(dataStr); //

            int sumTapNotes = inputJson._tapNotes.Length;
            int sumHoldNotes = inputJson._holdNotes.Length;
            for (int i = 0; i < sumTapNotes; i++) //ノーツの位置を一個ずつ配置していく
            {
                switch (inputJson._tapNotes[i]._type)
                {
                    case 1:
                        GetData(NotesType.Star, i);
                        break;
                    case 3:
                        GetData(NotesType.Nebula, i);
                        break;
                    case 4:
                        GetData(NotesType.Flare, i);
                        break;
                    default:
                        break;
                }
            }

            for (int i = 0; i < sumHoldNotes; i++) //ノーツの位置を一個ずつ配置していく
            {
                GetData(NotesType.Meteor, i);
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

            NotesManager.instance.SetBlockNotes(SortNotes);
            NotesController.instance.SetData(_blockHeight, _notesSeed);
            this.gameObject.SetActive(false);
            Debug.Log("Notes Generate Complate");

            await Task.Run(() => { }).ConfigureAwait(false);
        }

        private void GetData(NotesType type, int i)
        {
            GameObject notesObject;

            int block = 0;

            switch (type)
            {
                case NotesType.Star:
                    notesObject = _starNotesObject;
                    block = inputJson._tapNotes[i]._block;
                    break;
                case NotesType.Meteor:
                    notesObject = _meteorNotesObject;
                    block = inputJson._holdNotes[i]._block;
                    break;
                case NotesType.Nebula:
                    notesObject = _nebulaNotesObject;
                    block = inputJson._tapNotes[i]._block;
                    break;
                case NotesType.Flare:
                    notesObject = _flareNotesObject;
                    block = inputJson._tapNotes[i]._block;
                    break;
                default:
                    notesObject = null;
                    break;
            }

            if (notesObject == null) return;
            GameObject notesInstance = Instantiate(notesObject, new Vector3(block - 1.5f, 0.55f, _blockHeight + 1),
                notesObject.transform.rotation);
            notesInstance.transform.SetParent(NotesManager.instance.transform, false);
            Notes notes = notesInstance.GetComponent<Notes>();
            notes._block = block;
            notes._type = type;
            switch (type)
            {
                case NotesType.Star:
                    notes._time = inputJson._tapNotes[i]._time;
                    break;
                case NotesType.Meteor:
                    notes._time = inputJson._holdNotes[i]._time[0];
                    notes._endTime = inputJson._holdNotes[i]._time[1];
                    break;
                case NotesType.Nebula:
                    notes._time = inputJson._tapNotes[i]._time; //DEBUG
                    break;
                case NotesType.Flare:
                    notes._time = inputJson._tapNotes[i]._time; //DEBUG
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