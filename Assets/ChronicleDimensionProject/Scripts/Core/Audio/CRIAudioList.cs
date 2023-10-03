using System.Collections.Generic;
using CriWare;
using UnityEditor;
using UnityEngine;

public class CriAudioList : EditorWindow
{
    private readonly CriAtomWindowInfo projInfo = new CriAtomWindowInfo();
    [SerializeField] private string searchPath;
    private List<AudioListInfo> audioList = new List<AudioListInfo>();

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        audioList.Clear();

        var acfInfoList = projInfo.GetAcfInfoList(false, searchPath);
        foreach (var acfInfo in acfInfoList)
        {
            var info = new AudioListInfo();
            info.name = acfInfo.name;
            info.path = searchPath;
            info.type = AudioListType.ACF;
            audioList.Add(info);
        }

        var acbInfoList = projInfo.GetAcbInfoList(false, searchPath);
        foreach (var acbInfo in acbInfoList)
        {
            var info = new AudioListInfo();
            info.name = acbInfo.name;
            info.path = searchPath;
            info.type = AudioListType.ACB;
            audioList.Add(info);
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("Audio List", EditorStyles.boldLabel);
        foreach (var info in audioList)
        {
            EditorGUILayout.LabelField(info.name, info.path);
        }
    }
}

public enum AudioListType
{
    ACF,
    ACB
}

[System.Serializable]
public class AudioListInfo
{
    public string name;
    public string path;
    public AudioListType type;
}