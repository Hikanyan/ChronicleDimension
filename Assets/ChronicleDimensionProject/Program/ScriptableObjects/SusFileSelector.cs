using UnityEngine;
using UnityEditor;

public class SusFileSelector
{
    [MenuItem("Custom Tools/Select .sus File")]
    private static void SelectSusFile()
    {
        var path = EditorUtility.OpenFilePanel("Select .sus File", Application.dataPath, "sus");
        if (!string.IsNullOrEmpty(path))
        {
            Debug.Log("Selected .sus file path: " + path);
            // ここで選択されたファイルのパスを使用して、必要な処理を行う
        }
        else
        {
            Debug.Log("File selection cancelled.");
        }
    }
}