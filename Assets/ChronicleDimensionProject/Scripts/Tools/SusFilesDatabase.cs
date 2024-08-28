using UnityEngine;

[CreateAssetMenu(fileName = "SusFilesDatabase", menuName = "Sus/Files Database")]
public class SusFilesDatabase : ScriptableObject
{
    public SusFileEntry[] files;
}

[System.Serializable]
public class SusFileEntry
{
    public string susFilePath;
    public string jsonFilePath;
    public bool isConverted;
}