using ChronicleDimensionProject.Common;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using VContainer;
using VContainer.Unity;

namespace ChronicleDimensionProject.Boot
{
    /// <summary>
    /// 最初に必ず読み込まれるクラス
    /// </summary>
    public class BootLoader : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize()
        {
            // シーンの読み込みやAddressableでPrefabを読み込む処理
            LoadInitialPrefab();
        }

        // Addressableを使ったPrefabの読み込み
        static void LoadInitialPrefab()
        {
            var handle = Addressables.LoadAssetAsync<GameObject>("YourPrefabAddress");
            handle.Completed += OnPrefabLoaded;
        }

        // Prefabがロードされたときの処理
        static void OnPrefabLoaded(AsyncOperationHandle<GameObject> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                var prefab = handle.Result;
                Object.Instantiate(prefab);
            }
            else
            {
                Debug.LogError("Prefabの読み込みに失敗しました");
            }
        }
    }
}