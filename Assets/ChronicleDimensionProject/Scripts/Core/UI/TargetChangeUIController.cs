using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(InputUIButton))]
public class TargetChangeUIController : MonoBehaviour
{
    private InputUIButton _inputUIButton;
    [SerializeField] private GameObject target;

    private void Start()
    {
        TryGetComponent(out _inputUIButton);
        _inputUIButton.Target = target;

        // InputUIButtonのイベントにメソッドを登録
        _inputUIButton.OnButtonDown += HandleButtonDown;
        _inputUIButton.OnButtonUp += HandleButtonUp;
    }

    private void HandleButtonDown()
    {
        // パネルを表示
        UIManager.Instance.ShowPanel(target); // targetを型ではなくそのまま渡す
    }

    private void HandleButtonUp()
    {
        // パネルを非表示
        UIManager.Instance.HidePanel(target); // targetを型ではなくそのまま渡す
    }

    private void OnDestroy()
    {
        // イベントの購読解除
        _inputUIButton.OnButtonDown -= HandleButtonDown;
        _inputUIButton.OnButtonUp -= HandleButtonUp;
    }
}