using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChronicleDimensionProject.UI
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        private readonly Dictionary<Type, IUIView> _views = new Dictionary<Type, IUIView>();

        public void RegisterView<T>(T view) where T : IUIView
        {
            _views[typeof(T)] = view;
        }

        public async UniTask Show<T>() where T : IUIView
        {
            if (_views.TryGetValue(typeof(T), out var view))
            {
                await view.Show();
            }
            else
            {
                Debug.LogError($"{typeof(T)} not registered in UIManager");
            }
        }

        public async UniTask Hide<T>() where T : IUIView
        {
            if (_views.TryGetValue(typeof(T), out var view))
            {
                await view.Hide();
            }
            else
            {
                Debug.LogError($"{typeof(T)} not registered in UIManager");
            }
        }
    }
}