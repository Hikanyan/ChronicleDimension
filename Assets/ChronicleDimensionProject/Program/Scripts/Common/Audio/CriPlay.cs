using System.Collections;
using System.Collections.Generic;
using ChronicleDimensionProject.Common;
using UnityEngine;

namespace ChronicleDimensionProject.Common.Test
{
    public class CriPlay : MonoBehaviour
    {
        [SerializeField] private CriAudioManager.CueSheet cueSheet;
        [SerializeField] private string cueName;

        private void Start()
        {
            CriAudioManager.Instance.PlayBGM(cueSheet, cueName);
        }
    }
}