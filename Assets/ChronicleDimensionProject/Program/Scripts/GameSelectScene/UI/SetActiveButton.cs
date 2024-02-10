using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ChronicleDimensionProject.GameSelectScene.UI
{
    public class SetActiveButton : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _setActiveList;
        private InputUIButton _inputUIButton;

        void Start()
        {
            _inputUIButton = GetComponent<InputUIButton>();
            _inputUIButton.onClick.AddListener(SelectGameMode);
        }

        private void SelectGameMode()
        {
            foreach (var obj in _setActiveList)
            {
                InvertState(obj);
            }
        }

        // SetActiveを反転
        private void InvertState(GameObject obj)
        {
            bool currentState = obj.activeSelf;
            obj.SetActive(!currentState);
            Debug.Log($"Open{obj.name}");
        }
    }
}