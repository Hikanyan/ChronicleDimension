using System.IO;
using UnityEngine;

/// <summary>
/// SaveManagerを使用するとセーブデータを保存、読み込みすることができます。
/// </summary>
public class SaveManager
{
    //Singletonを使用するインスタンスを作成する
    private static SaveManager _instance = new SaveManager();
    //インスタンスを取得するためのプロパティ
    public static SaveManager Instance => _instance;

    //これをすることで外部からインスタンス化できない
    SaveManager()
    {
        //インスタンス化したときに必ずLoadGameを呼ぶ
        LoadGame();
    }

    //セーブデータのパス
    //=>はgetの省略形
    public string _savePath => Application.dataPath + "/savedata.json";
    
    //セーブデータのプロパティ
    public SaveData SaveData { get; private set; }

    //コンストラクタ
    public void SaveGame()
    {
        //セーブデータを保存する
        var json = JsonUtility.ToJson(SaveData);
        //writerを使ってjsonを保存する
        StreamWriter writer = new StreamWriter(_savePath, false);
        //書き込み
        writer.WriteLine(json);
        //Flush()を呼ぶことで書き込みが確定する
        writer.Flush();
        //閉じる
        writer.Close();
    }
    
    /// <summary>
    /// LoadGameを呼ぶとセーブデータを読み込むことができます。
    /// </summary>
    public void LoadGame()
    {
        //セーブデータがあるかどうかを判別する
        //なければ処理を終了する
        if (!File.Exists((_savePath)))
        {
            Debug.Log("セーブデータがありません。\n新しくセーブデータを作成します。");
            //セーブデータを作成する
            SaveData = new SaveData();
            //セーブデータを保存する
            SaveGame();
            return;
        }
        else
        {
            //セーブデータを読み込む
            //readerを使ってjsonを読み込む
            StreamReader reader = new StreamReader(_savePath);
            //読み込んだjsonをstringに変換する
            var jsonDate = reader.ReadToEnd();
            //stringをSaveDataに変換する
            SaveData = JsonUtility.FromJson<SaveData>(jsonDate);
            //readerを閉じる
            reader.Close();
        }
    }

    /// <summary>
    /// SeveDateDelete()はセーブデータを削除するメソッドです。
    /// </summary>
    public void SeveDateDelete()
    {
        // セーブデータが存在するかを判定する
        if (File.Exists(_savePath))
        {
            // セーブデータが存在する場合、削除する
            File.Delete(_savePath);
            Debug.Log("セーブデータが削除されました。");
        }
        else
        {
            Debug.Log("セーブデータは存在しません。");
        }
    }

}