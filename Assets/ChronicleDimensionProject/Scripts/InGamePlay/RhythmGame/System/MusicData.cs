using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

/// <summary>
/// MusicSelectからGameSceneに曲のデータを渡す用
/// </summary>
public class MusicData : MonoBehaviour
{
    [SerializeField] public CueSheet cueSheet = default;
    [SerializeField] public string musicName = default;
    [SerializeField] public AssetReferenceT<TextAsset> musicJsonReference;
    [SerializeField] public float delayTime = 0.0f;
    [SerializeField] public bool autoMode = false;
}