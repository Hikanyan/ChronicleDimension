using System;
using System.Collections.Generic;
using ChronicleDimensionProject.Scripts.Core.UI;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;


public class UIManager : AbstractSingleton<UIManager>
{
    private readonly Dictionary<Type, IUserInterface> _panels = new();

    /// <summary>  パネルの登録メソッド </summary>
    public void RegisterPanel<T>(T panel) where T : MonoBehaviour, IUserInterface
    {
        _panels[typeof(T)] = panel;
        // パネルの表示状態を監視して、表示状態が変化したらパネルの表示状態を変更する
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
        }).AddTo(panel); //パネルの破棄時に監視を解除する
    }

    /// <summary>  Panelの表示メソッド </summary>
    public void ShowPanel<T>() where T : IUserInterface
    {
        Type panelType = typeof(T);
        if (_panels.TryGetValue(panelType, out IUserInterface panel))
        {
            panel.IsVisible.Value = true;
        }
        else
        {
            Debug.LogWarning($"Panel of type {panelType} not found.");
        }
    }

    /// <summary>  パネルの非表示メソッド </summary>
    public void HidePanel<T>() where T : IUserInterface
    {
        Type panelType = typeof(T);
        if (_panels.TryGetValue(panelType, out IUserInterface panel))
        {
            panel.IsVisible.Value = false;
        }
        else
        {
            Debug.LogWarning($"Panel of type {panelType} not found.");
        }
    }
}