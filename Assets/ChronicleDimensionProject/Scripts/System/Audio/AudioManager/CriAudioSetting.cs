using System;
using UnityEngine;
using System.Collections.Generic;
using HikanyanLaboratory.System;

namespace HikanyanLaboratory.Audio
{
    [CreateAssetMenu(fileName = "CriAudioSetting", menuName = "HikanyanLaboratory/Audio/CriAudioSetting")]
    [Serializable]
    public class CriAudioSetting : ScriptableObject
    {
        [SerializeField] private string _streamingAssetsPathAcf;
        [SerializeField] private List<AudioCueSheet<string>> _audioCueSheet;

        public string StreamingAssetsPathAcf => _streamingAssetsPathAcf;
        public List<AudioCueSheet<string>> AudioCueSheet => _audioCueSheet;

        public void Initialize()
        {
            _audioCueSheet ??= new List<AudioCueSheet<string>>();
        }

        public void SearchCueSheet()
        {
            CriAudioLoader criAudioLoader = new CriAudioLoader();
            criAudioLoader.SetCriAudioSetting(this);
            criAudioLoader.SearchCueSheet();
        }

        public void SetStreamingAssetsPathAcf(string path)
        {
            _streamingAssetsPathAcf = path;
        }

        public void SetAudioCueSheet(List<AudioCueSheet<string>> cueSheets)
        {
            _audioCueSheet = cueSheets;
        }
    }
}
