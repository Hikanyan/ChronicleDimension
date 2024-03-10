using System.Collections;
using ChronicleDimensionProject.RhythmGameScene;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TestParseSusFileAddressables : MonoBehaviour
{
    // Addressableシステムで設定したSUSファイルのアドレス
    public string susFileAddress = "yourSusFileAddress";

    // StartメソッドでAddressableアセットのロードを開始
    void Start()
    {
        LoadAndParseSusFile(susFileAddress);
    }

    void LoadAndParseSusFile(string address)
    {
        Addressables.LoadAssetAsync<TextAsset>(address).Completed += OnSusFileLoaded;
    }

    // SUSファイルがロードされた後に呼ばれるコールバックメソッド
    void OnSusFileLoaded(AsyncOperationHandle<TextAsset> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            TextAsset susFile = handle.Result;
            // SUSファイルの内容を文字列として取得
            string fileContent = susFile.text;

            // ここでファイル内容を解析
            SusParser parser = new SusParser();
            SusData susData = parser.ParseSusString(fileContent); // ParseSusFileの代わりにParseSusStringとする

            parser.DebugLogSusData(susData);
        }
        else
        {
            Debug.LogError("Failed to load SUS file.");
        }
    }
}
