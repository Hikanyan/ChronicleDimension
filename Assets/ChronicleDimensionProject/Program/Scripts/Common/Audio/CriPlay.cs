using System.Collections;
using System.Collections.Generic;
using ChronicleDimension.Core;
using UnityEngine;

public class CriPlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CriAudioManager.Instance.PlayBGM(CriAudioManager.CueSheet.Bgm,"BGM_Running Through The Galaxy_BPM150 20220630");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
