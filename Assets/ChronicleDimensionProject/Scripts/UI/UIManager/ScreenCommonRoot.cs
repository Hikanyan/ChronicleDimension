using ChronicleDimensionProject.Dialog;
using UnityEngine;

namespace ChronicleDimensionProject.UI
{
    public class ScreenCommonRoot : MonoBehaviour
    {
        [SerializeField] private DialogManager _dialogManager;

        private void Awake()
        {
            if (_dialogManager == null)
            {
                Debug.LogError("DialogManager is not assigned.");
            }
        }
    }
}