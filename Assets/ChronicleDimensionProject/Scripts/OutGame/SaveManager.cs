using System.IO;
using UnityEngine;

/// <summary>
/// SaveManagerは、セーブデータの読み込み、保存、削除を行うためのシングルトンクラスです。
/// </summary>
public class SaveManager : AbstractSingleton<SaveManager>
{
    /// <summary> セーブデータの保存先パスを取得します。 </summary>
    string SavePath => Application.persistentDataPath + "/savedata.json";

    /// <summary> 現在のセーブデータを取得します。 </summary>
    public SaveData SaveData { get; private set; }

    /// <summary> 指定された型に基づいてセーブデータのパスを取得します。 </summary>
    static string GetPath<T>()
    {
        return Path.Combine(Application.persistentDataPath, $"{typeof(T)}.json");
    }

    /// <summary> 指定された型の設定をロードします。 </summary>
    public static T LoadSettings<T>() where T : new()
    {
        FileInfo info = new FileInfo(GetPath<T>());
        if (!info.Exists)
        {
            SaveSettings(new T());
        }

        string datastr;
#if UNITY_IOS && UNITY_EDITOR
        datastr = File.ReadAllText(GetPath<T>());
#else
        using (StreamReader reader = new StreamReader(GetPath<T>(), false))
        {
            datastr = reader.ReadToEnd();
        }
#endif
        return JsonUtility.FromJson<T>(datastr);
    }

    /// <summary> 指定された設定をセーブします。 </summary>
    public static void SaveSettings<T>(T setting)
    {
        string jsonstr = JsonUtility.ToJson(setting);
        Debug.Log(jsonstr);
#if UNITY_IOS && UNITY_EDITOR
        File.WriteAllText(GetPath<T>(), jsonstr);
#else
        using (StreamWriter writer = new StreamWriter(GetPath<T>(), false))
        {
            writer.Write(jsonstr);
        }
#endif
    }

    /// <summary> ゲームのセーブデータを保存します。 </summary>
    public void SaveGame()
    {
        string json = JsonUtility.ToJson(SaveData);
        using (StreamWriter writer = new StreamWriter(SavePath, false))
        {
            writer.WriteLine(json);
        }
    }

    /// <summary> ゲームのセーブデータをロードします。 </summary>
    public void LoadGame()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("セーブデータがありません。\n新しくセーブデータを作成します。");
            SaveData = new SaveData();
            SaveGame();
        }
        else
        {
            using (StreamReader reader = new StreamReader(SavePath))
            {
                string jsonDate = reader.ReadToEnd();
                SaveData = JsonUtility.FromJson<SaveData>(jsonDate);
            }
        }
    }

    /// <summary> セーブデータを削除します。 </summary>
    public void SaveDataDelete()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
            Debug.Log("セーブデータが削除されました。");
        }
        else
        {
            Debug.Log("セーブデータは存在しません。");
        }
    }
}