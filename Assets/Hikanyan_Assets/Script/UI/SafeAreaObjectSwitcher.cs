using UnityEngine;

public class SafeAreaObjectSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject windowsObject;        // Windows環境で使用するGameObject
    [SerializeField] private GameObject simulatorObject;      // Simulator環境で使用するGameObject

    private void Start()
    {
        if (Application.isMobilePlatform)
        {
            // セーフエリアに合わせたRectTransformを持つゲームオブジェクトをアクティブにする
            simulatorObject.SetActive(true);
            windowsObject.SetActive(false);
        }
        else
        {
            // 通常のRectTransformを持つゲームオブジェクトをアクティブにする
            simulatorObject.SetActive(false);
            windowsObject.SetActive(true);
        }
    }
}
