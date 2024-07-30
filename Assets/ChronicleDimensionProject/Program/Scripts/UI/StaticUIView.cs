using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChronicleDimensionProject.UI
{
    /// <summary>
    /// 静的なUIの表示・非表示を管理するクラス
    /// </summary>
    public class StaticUIView : MonoBehaviour, IUIView
    {
        public virtual UniTask Show()
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public virtual UniTask Hide()
        {
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }
    }
}