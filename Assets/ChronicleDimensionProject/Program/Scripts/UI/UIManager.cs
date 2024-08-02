﻿using System;
using System.Collections.Generic;
using ChronicleDimensionProject.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChronicleDimensionProject.UI
{
    public class UIManager : AbstractSingletonMonoBehaviour<UIManager>
    {
        private readonly Dictionary<Type, Node> _nodes = new Dictionary<Type, Node>();
        private readonly Dictionary<Type, IUIView> _views = new Dictionary<Type, IUIView>();
        private readonly Dictionary<Type, object> _presenters = new Dictionary<Type, object>();

        public void RegisterNode(Node node)
        {
            _nodes[node.GetType()] = node;
        }

        public void RegisterView<T>(T view) where T : IUIView
        {
            _views[typeof(T)] = view;
        }

        public void RegisterPresenter<TPresenter, TView>(TView view, SampleUIModel model)
            where TPresenter : class
            where TView : IUIView
        {
            var presenter = (TPresenter)Activator.CreateInstance(typeof(TPresenter), view, model);
            _presenters[typeof(TPresenter)] = presenter;
        }

        public TPresenter GetPresenter<TPresenter>() where TPresenter : class
        {
            if (_presenters.TryGetValue(typeof(TPresenter), out var presenter))
            {
                return (TPresenter)presenter;
            }

            Debug.LogError($"{typeof(TPresenter)} not registered in UIManager");
            return null;
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

        public async UniTask ShowPresenter<TPresenter>() where TPresenter : class
        {
            if (_presenters.TryGetValue(typeof(TPresenter), out var presenter))
            {
                var showMethod = typeof(TPresenter).GetMethod("Show");
                if (showMethod != null)
                {
                    await (UniTask)showMethod.Invoke(presenter, null);
                }
            }
            else
            {
                Debug.LogError($"{typeof(TPresenter)} not registered in UIManager");
            }
        }

        public async UniTask HidePresenter<TPresenter>() where TPresenter : class
        {
            if (_presenters.TryGetValue(typeof(TPresenter), out var presenter))
            {
                var hideMethod = typeof(TPresenter).GetMethod("Hide");
                if (hideMethod != null)
                {
                    await (UniTask)hideMethod.Invoke(presenter, null);
                }
            }
            else
            {
                Debug.LogError($"{typeof(TPresenter)} not registered in UIManager");
            }
        }
    }
}