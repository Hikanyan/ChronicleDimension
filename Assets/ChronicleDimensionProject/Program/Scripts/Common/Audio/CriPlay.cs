using System.Collections;
using System.Collections.Generic;
using ChronicleDimensionProject.Common;
using UnityEngine;

namespace ChronicleDimensionProject.Common.Test
{
    public class CriPlay : MonoBehaviour
    {
        [SerializeField] CriAudioManager.CueSheet cueSheet;
        [SerializeField] string cueName;

        private void Start()
        {
            CriAudioManager.Instance.PlayBGM(cueSheet, cueName);
        }
    }
}