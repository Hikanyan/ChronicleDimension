using System.Collections;
using System.Collections.Generic;
using ChronicleDimension.Common;
using UnityEngine;

namespace ChronicleDimensionProject.Common.Test
{
    public class CriPlay : MonoBehaviour
    {
        [SerializeField] CriAudioManager.CueSheet cueSheet;
        [SerializeField] string cueName;

        private void Start()
        {
            Debug.Log("Play BGM");
            CriAudioManager.Instance.PlayBGM(cueSheet, cueName);
        }
    }
}