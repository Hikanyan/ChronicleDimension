using System.Collections.Generic;
using UnityEngine;

namespace ChronicleDimensionProject.UI
{
    public class GenerateMusicOpenButton : MonoBehaviour
    {
        [SerializeField] private GameObject musicOpenButtonPrefab;
        [SerializeField] private List<GameObject> musicDataList;
        
        
        //すくりたぶるおぶじぇくとのデータからボタンを生成する
        public void GenerateMusicOpenButtonFromData()
        {
            foreach (var musicData in musicDataList)
            {
                var musicOpenButton = Instantiate(musicOpenButtonPrefab, transform);
                musicOpenButton.GetComponent<MusicOpenButton>().SetMusicData(musicData);
            }
        }
        
        //ボタンを押したときの処理
        public void OnClick()
        {
            //ボタンを押したときの処理
        }
        
        
    }
}