using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChronicleDimensionProject.Data
{
    [CreateAssetMenu(menuName = "RhythmGame/MusicGameDataList")]
    public class MusicGameDataList : ScriptableObject
    {
        [SerializeField] private List<MusicGameData> _musicGameDataList = new List<MusicGameData>();

        public List<MusicGameData> GetMusicGameDataList()
        {
            return _musicGameDataList;
        }
    }
}