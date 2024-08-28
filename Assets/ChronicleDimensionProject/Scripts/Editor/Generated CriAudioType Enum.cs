using HikanyanLaboratory.Audio;
using UnityEditor;
using UnityEngine;

namespace Hikanyan.Core
{
    public class GeneratedCriAudioTypeEnum
    {
        [MenuItem("HikanyanTools/CriAudio/Generate CriAudioType Enum")]
        public static void GenerateCriAudioType()
        {
            // ScriptableObjectのインスタンスをロード
            string assetPath = "Assets/ChronicleDimensionProject/GameData/ScriptableObject/Audio/CriAudioSetting.asset";
            
            CriAudioSetting criAudioSetting = AssetDatabase.LoadAssetAtPath<CriAudioSetting>(assetPath);

            if (criAudioSetting != null)
            {
                CriAudioLoader criAudioLoader = new CriAudioLoader();
                criAudioLoader.SetCriAudioSetting(criAudioSetting);
                
                criAudioLoader.Initialize();
                criAudioLoader.GenerateEnumFile();
                
                Debug.Log("CriAudioType enum has been generated.");
            }
            else
            {
                Debug.LogError("CriAudioSetting asset not found at " + assetPath);
            }
        }
    }
}