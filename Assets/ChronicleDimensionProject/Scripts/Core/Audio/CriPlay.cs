using System.Collections;
using System.Collections.Generic;
using ChronicleDimension.Core;
using UnityEngine;

public class CriPlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CriAudioManager.Instance.PlayBGM(CriAudioManager.CueSheet.Bgm,"");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
