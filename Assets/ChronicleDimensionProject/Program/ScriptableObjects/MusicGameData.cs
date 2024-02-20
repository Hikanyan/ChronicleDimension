using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ChronicleDimensionProject.Data
{
    [CreateAssetMenu(menuName = "RhythmGame/MusicGameData")]
    public class MusicGameData : ScriptableObject
    {
        [SerializeField] private MusicDifficultyLevel _musicDifficultyLevel;
        [SerializeField] private string _musicName;
        [SerializeField] public AssetReferenceT<TextAsset> _musicSusReference;
        [SerializeField] public float _delayTime = 0.0f;
    }
}