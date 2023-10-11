using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class UIManager : AbstractSingleton<UIManager>
{
    public GameObject uiPrefab; // UIのプレハブ
    private Dictionary<string, GameObject> uiDictionary; // UIのインスタンスを管理する辞書

    protected override void OnAwake()
    {
        uiDictionary = new Dictionary<string, GameObject>(); // UIのインスタンスを格納する辞書を初期化

        // UIプレハブをインスタンス化し、各UIを表示する
        GameObject uiObject = Instantiate(uiPrefab);
        uiObject.name = uiPrefab.name;

        // 各UIを取得して表示
        foreach (Transform child in uiObject.transform)
        {
            string uiName = child.gameObject.name;
            uiDictionary.Add(uiName, child.gameObject);
            child.gameObject.SetActive(false);
        }
    }

    public async UniTask AddUI<T>(GameObject uiPrefab) where T : UIBase
    {
        string uiName = typeof(T).Name;
        if (!uiDictionary.ContainsKey((uiName)))
        {
            GameObject uiObject = await InstantiateUI(uiName, uiPrefab);
            uiDictionary.Add(uiName, uiObject);
        }
        else
        {
            Debug.Log($"{uiName} UI already exists in the dictionary.");
        }
    }

    // 指定されたUIを表示するメソッド
    public async UniTask OpenUI<T>() where T : UIBase
    {
        string uiName = typeof(T).Name; // ジェネリック型からUIの名前を取得

        if (!uiDictionary.ContainsKey(uiName))
        {
            // UIが辞書に存在しない場合は、UIを生成して辞書に追加
            GameObject uiObject = await InstantiateUI(uiName);
            uiDictionary.Add(uiName, uiObject);
        }
        else
        {
            // UIが辞書に存在する場合は、UIをアクティブにする
            GameObject uiObject = uiDictionary[uiName];
            uiObject.SetActive(true);
        }
    }

    // 指定されたUIを非表示にするメソッド
    public void CloseUI<T>() where T : UIBase
    {
        string uiName = typeof(T).Name; // ジェネリック型からUIの名前を取得

        if (uiDictionary.ContainsKey(uiName))
        {
            // UIが辞書に存在する場合は、UIを非アクティブにする
            GameObject uiObject = uiDictionary[uiName];
            uiObject.SetActive(false);
        }
    }

    private async UniTask<GameObject> InstantiateUI(string uiName)
    {
        return await InstantiateUI(uiName, null);
    }

    private async UniTask<GameObject> InstantiateUI(string uiName, GameObject uiPrefab)
    {
        GameObject uiObject;

        if (uiPrefab != null)
        {
            uiObject = Instantiate(uiPrefab); // プレハブからインスタンスを生成し、親オブジェクトの下に配置
        }
        else
        {
            uiObject = (GameObject)await Resources.LoadAsync(uiName).ToUniTask(); // UIのプレハブを非同期にロード
        }

        uiObject.name = uiName; // UIの名前を設定

        await uiObject.transform.DOScale(Vector3.zero, 0f).From().SetEase(Ease.OutBack).AsyncWaitForCompletion();
        // UIのスケールをゼロからアニメーションで徐々に拡大する

        // UIの表示
        uiObject.SetActive(true);

        return uiObject; // 生成したUIのインスタンスを返す
    }

}

