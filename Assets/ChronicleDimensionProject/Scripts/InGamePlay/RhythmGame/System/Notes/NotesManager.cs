using System;
using UnityEngine;
using UnityEngine.Serialization;

public class NotesManager: AbstractSingleton<NotesManager>
{
    [SerializeField] public string perfectSoundName;
    [SerializeField] public string greatSoundName;
    [SerializeField] public string goodSoundName;
    [SerializeField] public string missSoundName;
    [SerializeField] public string noneTapSoundName;
    [SerializeField] public string holdSoundName;
    
    bool _autoMode;
    float _judgeOffset;

    private NotesGenerator NotesGenerator;
    private NotesController NotesController;

    private void Start()
    {
        // PlayerSettings settings = SaveLoad.LoadSettings<PlayerSettings>();
        // _autoMode = RhythmGameManager.Instance.AutoMode;
        // _judgeOffset = settings.judgeOffset;
    }
    
    
}