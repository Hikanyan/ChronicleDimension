using System;
using System.Collections.Generic;
using ChronicleDimensionProject.Common;
using ChronicleDimensionProject.Scripts.Core.UI;
using UnityEngine;
using UniRx;


public class UIManager : AbstractSingletonMonoBehaviour<UIManager>
{
    protected override bool UseDontDestroyOnLoad => true;
    private readonly Dictionary<Type, IUserInterface> _panels = new Dictionary<Type, IUserInterface>();

    public void RegisterPanel(IUserInterface panel)
    {
        Type panelType = panel.GetType();
        _panels[panelType] = panel;

        //AddToの代わりにCompositeDisposableを使うこともできます。
        CompositeDisposable compositeDisposable = new CompositeDisposable();

        panel.IsVisible.Subscribe(isVisible =>
        {
            if (isVisible)
            {
                panel.Show();
            }
            else
            {
                panel.Hide();
            }
        }).AddTo(compositeDisposable);

        // ここで compositeDisposable を Dispose することで、手動で解放できます
        // compositeDisposable.Dispose();
    }

    public void ShowPanel(IUserInterface panel)
    {
        Type panelType = panel.GetType();
        if (_panels.TryGetValue(panelType, out IUserInterface registeredPanel))
        {
            registeredPanel.IsVisible.Value = true;
        }
        else
        {
            Debug.LogWarning($"Panel of type {panelType} not found.");
        }
    }

    public void HidePanel(IUserInterface panel)
    {
        Type panelType = panel.GetType();
        if (_panels.TryGetValue(panelType, out IUserInterface registeredPanel))
        {
            registeredPanel.IsVisible.Value = false;
        }
        else
        {
            Debug.LogWarning($"Panel of type {panelType} not found.");
        }
    }
}