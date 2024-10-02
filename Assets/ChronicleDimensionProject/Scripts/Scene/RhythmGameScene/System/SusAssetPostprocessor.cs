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
            var text = File.ReadAllText(ctx.assetPath);
            TextAsset textAsset = new TextAsset(text);
            ctx.AddObjectToAsset("Sus", textAsset);
            ctx.SetMainObject(textAsset);
        }
    }
}