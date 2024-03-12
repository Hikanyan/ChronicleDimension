using System;
using ChronicleDimensionProject.Boot;
using ChronicleDimensionProject.GameSelectScene;
using ChronicleDimensionProject.Title;
using UnityEngine;

namespace ChronicleDimensionProject.Common.UI
{
    public class ButtonUIController : MonoBehaviour
    {
        [SerializeField] GameState gameState = default;
        private CriAudioManager.CueSheet cueSheet;

        public void ClickSeButtan(string name)
        {
            CriAudioManager.Instance.PlaySE(cueSheet, name);
        }

        public void ChangeState()
        {
            gameState = GameState.GameSelect;
            GameManager.Instance.Change(gameState);
        }


        public void SetActive(GameObject setActiveObject)
        {
            setActiveObject.SetActive(!setActiveObject.activeSelf);
        }
    }
}