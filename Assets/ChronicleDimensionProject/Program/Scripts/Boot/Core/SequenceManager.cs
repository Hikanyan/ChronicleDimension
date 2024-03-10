using ChronicleDimensionProject.Common;
using UnityEngine;

namespace ChronicleDimensionProject.Boot
{
    /// <summary> このシングルトンは、観察されたゲームやイベントに基づいてゲームの状態を決定します。 </summary>
    public class SequenceManager : AbstractSingletonMonoBehaviour<SequenceManager>
    {
        [SerializeField] GameObject[] _preloadedAssets;

        /// <summary> 初期化 </summary>
        public void Initialize()
        {
            InstantiatePreloadedAssets();
        }

        /// <summary> 登録されたPrefabを全てインスタンス化 </summary>
        private void InstantiatePreloadedAssets()
        {
            foreach (var asset in _preloadedAssets)
            {
                Instantiate(asset);
            }
        }
    }
}