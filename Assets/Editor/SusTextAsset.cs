using System;
using System.IO;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class SusTextAsset
{

    public const string Extension = ".sus";

    [SerializeField] private string path;

    [SerializeField] private string textString;

    [SerializeField] private string byteString;

    public string Text => textString;

    public byte[] Bytes => Encoding.ASCII.GetBytes(byteString);

    public static implicit operator TextAsset(SusTextAsset textAsset)
    {
        return new TextAsset(textAsset.textString);
    }

    public static implicit operator SusTextAsset(TextAsset textAsset)
    {
        return new SusTextAsset { textString = textAsset.text };
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(SusTextAsset))]
public class SusInspectorEditor : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var path = property.FindPropertyRelative("path").stringValue;
        var loaded = AssetDatabase.LoadAssetAtPath(path, typeof(Object));
        var field = EditorGUI.ObjectField(position, label, loaded, typeof(Object), false);
        var loadPath = AssetDatabase.GetAssetPath(field);
        var fileExtension = Path.GetExtension(loadPath);
        if (field == null || fileExtension != SusTextAsset.Extension)
        {
            property.Set("path", "");
            property.Set("textString", "");
            property.Set("byteString", "");
        }
        else
        {
            var pathProperty = property.FindPropertyRelative("path");
            property.Set("path", loadPath.Substring(loadPath.IndexOf("Assets", StringComparison.Ordinal)));
            property.Set("textString", File.ReadAllText(pathProperty.stringValue));
            property.Set("byteString", Encoding.ASCII.GetString(File.ReadAllBytes(pathProperty.stringValue)));
        }
    }
}

public static class SerializedPropertyExtension
{
    public static void Set(this SerializedProperty property, string name, string value)
    {
        var pathProperty = property.FindPropertyRelative(name);
        pathProperty.stringValue = value;
    }
}

#endif
