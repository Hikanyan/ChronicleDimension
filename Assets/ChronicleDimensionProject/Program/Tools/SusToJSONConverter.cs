using System.Collections.Generic;
using UnityEngine;

public class SusToJSONConverter : MonoBehaviour
{
    [SerializeField]
    private string inputFilePath = "path_to_your_sus_file.sus";
    [SerializeField]
    private string outputFileName = "output.json";

    void Start()
    {
        string inputPath = System.IO.Path.Combine(Application.streamingAssetsPath, inputFilePath);
        string content = System.IO.File.ReadAllText(inputPath);
        var result = ConvertSusToJson(content);
        string jsonOutput = JsonUtility.ToJson(result, true);

        SaveJsonToFile(jsonOutput, outputFileName);
    }

    private SusData ConvertSusToJson(string content)
    {
        SusData data = new SusData
        {
            metadata = new Dictionary<string, string>(),
            musicData = new Dictionary<string, string>(),
            requests = new List<Dictionary<string, string>>()
        };

        var lines = content.Split('\n');
        foreach (var line in lines)
        {
            string trimmedLine = line.Trim();
            if (trimmedLine.StartsWith("#"))
            {
                Debug.Log("Processing line: " + trimmedLine);  // デバッグログを出力
                if (trimmedLine.Contains(":"))
                {
                    var parts = trimmedLine.Split(new char[] { ':' }, 2);
                    string key = parts[0].TrimStart('#').Trim();
                    string value = parts[1].Trim();
                    Debug.Log("Key: " + key + ", Value: " + value);  // キーと値の確認
                    data.musicData[key] = value;
                }
                else if (trimmedLine.StartsWith("#REQUEST"))
                {
                    string command = "REQUEST";
                    string value = trimmedLine.Split(new char[] { ' ' }, 2)[1].Trim('"');
                    data.requests.Add(new Dictionary<string, string> { { command, value } });
                }
                else
                {
                    var parts = trimmedLine.Split(new char[] { ' ' }, 2);
                    string command = parts[0].TrimStart('#').Trim();
                    string value = parts.Length > 1 ? parts[1].Trim('"') : "";
                    data.metadata[command] = value;
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

[System.Serializable]
public class SusData
{
    public Dictionary<string, string> metadata;
    public Dictionary<string, string> musicData;
    public List<Dictionary<string, string>> requests;
}
