using UnityEngine;

namespace ChronicleDimensionProject.Boot
{
    /// <summary> ゲームの初期化を行う </summary>
    public class BootLoader : MonoBehaviour
    {
        [SerializeField] private SequenceManager sequenceManagerPrefab;

        private void Awake()
        {
            Instantiate(sequenceManagerPrefab);
            SequenceManager.Instance.Initialize();
        }
    }
}