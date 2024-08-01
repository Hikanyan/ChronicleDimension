using System;
using UnityEngine;
using UnityEngine.UI;

namespace ChronicleDimensionProject.UI
{
    /// <summary>
    /// 静的なUIの表示・非表示を管理するクラス
    /// sample
    /// </summary>
    public class SampleStaticUIView : StaticUIView
    {
        // ここに処理を追加
        [SerializeField] Button _showButton;
        [SerializeField] Button _hideButton;

        private void Start()
        {
            _showButton.onClick.AddListener(() => { Show(); });
            _hideButton.onClick.AddListener(() => { Hide(); });
        }
    }
}