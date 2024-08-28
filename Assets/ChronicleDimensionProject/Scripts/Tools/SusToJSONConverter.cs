using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SusToJSONConverter : MonoBehaviour
{
    [SerializeField] private string inputFilePath = "path_to_your_sus_file.sus";
    [SerializeField] private string outputFileName = "output.json";

    void Start()
    {
        string inputPath = System.IO.Path.Combine(Application.streamingAssetsPath, inputFilePath);
        if (System.IO.File.Exists(inputPath))
        {
            string content = System.IO.File.ReadAllText(inputPath);
            var result = ConvertSusToJson(content);
            string jsonOutput = JsonUtility.ToJson(result, true);

            SaveJsonToFile(jsonOutput, outputFileName);
        }
        else
        {
            Debug.LogError("File not found: " + inputPath);
        }
    }

    public string ConvertSusFileToJson(string filePath)
    {
        string content = File.ReadAllText(filePath);
        SusData data = ConvertSusToJson(content);
        return JsonUtility.ToJson(data, true);
    }

    private SusData ConvertSusToJson(string content)
    {
        SusData data = new SusData
        {
            metadata = new List<MetadataEntry>(),
            musicData = new List<MusicDataEntry>(),
            requests = new List<RequestEntry>()
        };

        var lines = content.Split('\n');
        foreach (var line in lines)
        {
            string trimmedLine = line.Trim();
            if (trimmedLine.StartsWith("#"))
            {
                Debug.Log("Processing line: " + trimmedLine); // デバッグログを出力
                if (trimmedLine.Contains(":"))
                {
                    var parts = trimmedLine.Split(new char[] { ':' }, 2);
                    string key = parts[0].TrimStart('#').Trim();
                    string value = parts[1].Trim();
                    data.musicData.Add(new MusicDataEntry { key = key, value = value });
                }
                else if (trimmedLine.StartsWith("#REQUEST"))
                {
                    string command = "REQUEST";
                    string value = trimmedLine.Split(new char[] { ' ' }, 2)[1].Trim('"');
                    data.requests.Add(new RequestEntry { command = command, value = value });
                }
                else
                {
                    var parts = trimmedLine.Split(new char[] { ' ' }, 2);
                    string command = parts[0].TrimStart('#').Trim();
                    string value = parts.Length > 1 ? parts[1].Trim('"') : "";
                    data.metadata.Add(new MetadataEntry { key = command, value = value });
                }
            }
        }

        return data;
    }

    private void SaveJsonToFile(string json, string fileName)
    {
        string folderPath = System.IO.Path.Combine(Application.dataPath, "MusicData");
        if (!System.IO.Directory.Exists(folderPath))
            System.IO.Directory.CreateDirectory(folderPath);

        string filePath = System.IO.Path.Combine(folderPath, fileName);
        System.IO.File.WriteAllText(filePath, json);
        Debug.Log("JSON saved to " + filePath);
    }
}

/// <summary>
/// JSONに変換するためのデータクラス
/// </summary>
[System.Serializable]
public class SusData
{
    public List<MetadataEntry> metadata;
    public List<MusicDataEntry> musicData;
    public List<RequestEntry> requests;
}

[System.Serializable]
public class MetadataEntry
{
    public string key;
    public string value;
}

[System.Serializable]
public class MusicDataEntry
{
    public string key;
    public string value;
}

[System.Serializable]
public class RequestEntry
{
    public string command;
    public string value;
}