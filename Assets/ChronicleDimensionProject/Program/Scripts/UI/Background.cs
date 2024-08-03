using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChronicleDimensionProject.UI
{
    public class Background : MonoBehaviour, IUIView
    {
        private GameObject _background;

        public void Start()
        {
            TryGetComponent(out _background);
            
        }

        public UniTask Show()
        {
            throw new System.NotImplementedException();
        }

        public UniTask Hide()
        {
            throw new System.NotImplementedException();
        }
    }
}