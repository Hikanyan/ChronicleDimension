using System;
using System.IO;
using UnityEngine;

namespace ChronicleDimensionProject.Common
{
    public static class SaveLoad
    {
        static string GetPath<T>()
        {
            return Path.Combine(Application.persistentDataPath, $"{typeof(T).Name}.json");
        }

        public static T LoadSettings<T>() where T : new()
        {
            string path = GetPath<T>();
            if (!File.Exists(path))
            {
                SaveSettings(new T());
                return new T();
            }

            string dataStr = "";

            try
            {
                using StreamReader reader = new StreamReader(path);
                dataStr = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load file at {path}: {ex.Message}");
                return new T(); // エラー時は新しいインスタンスを返す
            }

            return JsonUtility.FromJson<T>(dataStr);
        }

        public static void SaveSettings<T>(T setting)
        {
            string jsonStr = JsonUtility.ToJson(setting);

            try
            {
                using StreamWriter writer = new StreamWriter(GetPath<T>(), false);
                writer.Write(jsonStr);
                writer.Flush();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to save file: {ex.Message}");
            }
        }
    }
}