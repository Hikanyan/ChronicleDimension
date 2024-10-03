using System.IO;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace ChronicleDimensionProject.RhythmGame
{
    /// <summary>
    /// susファイルをTextAssetに含めるためのポストプロセッサ
    /// </summary>
    [ScriptedImporter(1, "sus")]
    public class SusAssetPostprocessor : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            try
            {
                // UnityのAssetDatabase経由でファイルを読み込む
                var text = File.ReadAllText(ctx.assetPath);

                // TextAssetとして読み込み
                TextAsset textAsset = new TextAsset(text);

                // オブジェクトを追加
                ctx.AddObjectToAsset("Sus", textAsset);
                ctx.SetMainObject(textAsset);
            }
            catch (IOException e)
            {
                Debug.LogError($"ファイルの読み込みに失敗しました: {ctx.assetPath}\nエラー: {e.Message}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"予期しないエラーが発生しました: {e.Message}");
            }
        }
    }
}