using System;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    //Singletonにする
    private static SaveManager _instance;
    private string saveFilePath;
    public static SaveManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SaveManager>();
            }

            return _instance;
        }
    }


    private void Start()
    {
        // セーブデータの保存先を設定
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");
        //セーブデータの読み込み
        LoadGame();
    }

    public void SaveGame()
    {
        // ゲームの進行状況を保存するデータを作成
        SaveData saveData = new SaveData();
        // ここで必要なデータをsaveDataに設定する

        // JSON形式に変換
        string jsonData = JsonUtility.ToJson(saveData);

        // ファイルに保存
        File.WriteAllText(saveFilePath, jsonData);
    }

    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            // ファイルからデータを読み込み
            string jsonData = File.ReadAllText(saveFilePath);

            // JSONデータを復元
            SaveData saveData = JsonUtility.FromJson<SaveData>(jsonData);

            // ここで必要なデータをゲームに反映する
            //JSONデータから読み込んだデータをセーブデータに反映
            saveData.saveData[0]+= saveData.saveData[0];
            
        }
        else
        {
            Debug.Log("セーブデータが存在しません。新しいゲームを開始します。");
        }
    }
}

