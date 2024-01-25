using System.Collections.Generic;
using ChronicleDimensionProject.Scripts.Core.UI;
using UnityEngine;
//presenter としての役割を持つ
[RequireComponent(typeof(InputUIButton))]
public class ButtonController : MonoBehaviour
{
    private InputUIButton _inputUIButton;
    private IUserInterface _targetPanel;

    private void Start()
    {
        TryGetComponent(out _inputUIButton);
        UIManager.Instance.RegisterPanel(_targetPanel);
        // InputUIButtonのイベントにメソッドを登録
        _inputUIButton.OnButtonDown += HandleButtonDown;
        _inputUIButton.OnButtonUp += HandleButtonUp;
    }

    private void HandleButtonDown()
    {
    }

    private void HandleButtonUp()
    {
        UIManager.Instance.ShowPanel(_targetPanel);
    }
    private void OnDestroy()
    {
        // イベントの購読解除
        _inputUIButton.OnButtonDown -= HandleButtonDown;
        _inputUIButton.OnButtonUp -= HandleButtonUp;
    }
}