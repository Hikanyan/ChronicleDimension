using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;
public class GachaUI : UIBase
{
    // Gacha UIの実装
    
    [SerializeField] Image resultImage;

    private void Start()
    {
        // 初期状態で非表示にする
        resultImage.gameObject.SetActive(false);
    }

    public void  ShowResult(GachaItem item)
    {
        // ガチャの結果を非同期に処理

        // 結果の表示

        // 結果のアニメーション
    }
}