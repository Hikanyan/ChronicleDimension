using System.IO;
using UnityEngine;

/// <summary>
/// SaveManagerを使用するとセーブデータを保存、読み込みすることができます。
/// </summary>

public class SaveManager
{
    static string GetPath<T>()
    {
        return Path.Combine(Application.persistentDataPath, $"{typeof(T)}b.json");
    }

    public static T LoadSettings<T>() where T : new()
    {
        FileInfo info = new FileInfo(GetPath<T>());
        if (!info.Exists)
        {
            SaveSettings<T>(new T());
        }

        string datastr = "";

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


    // //Singletonを使用するインスタンスを作成する
    // private static SaveManager _instance = new SaveManager();
    // //インスタンスを取得するためのプロパティ
    // public static SaveManager Instance => _instance;
    //
    // //これをすることで外部からインスタンス化できない
    // SaveManager()
    // {
    //     //インスタンス化したときに必ずLoadGameを呼ぶ
    //     LoadGame();
    // }
    //
    // //セーブデータのパス
    // //=>はgetの省略形
    // string SavePath => Application.dataPath + "/savedata.json";
    //
    // //セーブデータのプロパティ
    // public SaveData SaveData { get; private set; }
    //
    // //コンストラクタ
    // public void SaveGame()
    // {
    //     //セーブデータを保存する
    //     var json = JsonUtility.ToJson(SaveData);
    //     //writerを使ってjsonを保存する
    //     StreamWriter writer = new StreamWriter(SavePath, false);
    //     //書き込み
    //     writer.WriteLine(json);
    //     //Flush()を呼ぶことで書き込みが確定する
    //     writer.Flush();
    //     //閉じる
    //     writer.Close();
    // }
    //
    // /// <summary>
    // /// LoadGameを呼ぶとセーブデータを読み込むことができます。
    // /// </summary>
    // public void LoadGame()
    // {
    //     //セーブデータがあるかどうかを判別する
    //     //なければ処理を終了する
    //     if (!File.Exists((SavePath)))
    //     {
    //         Debug.Log("セーブデータがありません。\n新しくセーブデータを作成します。");
    //         //セーブデータを作成する
    //         SaveData = new SaveData();
    //         //セーブデータを保存する
    //         SaveGame();
    //     }
    //     else
    //     {
    //         //セーブデータを読み込む
    //         //readerを使ってjsonを読み込む
    //         StreamReader reader = new StreamReader(SavePath);
    //         //読み込んだjsonをstringに変換する
    //         var jsonDate = reader.ReadToEnd();
    //         //stringをSaveDataに変換する
    //         SaveData = JsonUtility.FromJson<SaveData>(jsonDate);
    //         //readerを閉じる
    //         reader.Close();
    //     }
    // }
    //
    // /// <summary>
    // /// SeveDateDelete()はセーブデータを削除するメソッドです。
    // /// </summary>
    // public void SeveDateDelete()
    // {
    //     // セーブデータが存在するかを判定する
    //     if (File.Exists(SavePath))
    //     {
    //         // セーブデータが存在する場合、削除する
    //         File.Delete(SavePath);
    //         Debug.Log("セーブデータが削除されました。");
    //     }
    //     else
    //     {
    //         Debug.Log("セーブデータは存在しません。");
    //     }
    // }
}