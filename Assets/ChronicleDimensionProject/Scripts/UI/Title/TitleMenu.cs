using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ChronicleDimensionProject.UI
{
    public class TitleMenu : ScreenNode
    {
        [SerializeField] private Button _cashClearButton;
        [SerializeField] private Button _creditButton;
        [SerializeField] private Button _handoverButton;
        [SerializeField] private Button _supportButton;
        [SerializeField] private Button _backButton;


        private void Start()
        {
            _cashClearButton.onClick.AddListener(() =>
            {
                Debug.Log("CashClear");
            });
            _creditButton.onClick.AddListener(() =>
            {
                Debug.Log("Credit");
            });
            _handoverButton.onClick.AddListener(() =>
            {
                Debug.Log("Handover");
            });
            _supportButton.onClick.AddListener(() =>
            {
                Debug.Log("Support");
            });
            _backButton.onClick.AddListener(() =>
            {
                Debug.Log("Back");
            });
        }
    }
}