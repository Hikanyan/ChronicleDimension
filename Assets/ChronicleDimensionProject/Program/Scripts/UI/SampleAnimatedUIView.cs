using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ChronicleDimensionProject.UI
{
    /// <summary>
    /// アニメーション付きのUIの表示・非表示を管理するクラス
    ///  sample
    /// </summary>
    public class SampleAnimatedUIView : AnimatedUIView
    {
        [SerializeField] private Text _titleText;
        [SerializeField] private Text _countText;

        public void SetTitle(string title)
        {
            if (_titleText != null)
            {
                _titleText.text = title;
            }
        }

        public void SetCount(int count)
        {
            if (_countText != null)
            {
                _countText.text = count.ToString();
            }
        }
    }
}