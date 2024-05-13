using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace ChronicleDimensionProject.Editor
{
    public class SusToJsonEditor : EditorWindow
    {
        private StringBuilder _stringBuilder = new StringBuilder(512);
        private string _targetExtension = ".sus";
        private List<string> _filePaths = new List<string>();
        private Vector2 _scrollPosition;

        SusFilesDatabase database;

        [MenuItem("HikanyanTools/SusToJsonConverter")]
        private static void ShowWindow()
        {
            var window = GetWindow<SusToJsonEditor>();
            window.titleContent = new GUIContent("SusToJsonConverter");
            window.minSize = new Vector2(400, 300);
        }

        void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Database:", GUILayout.Width(70));
            database = (SusFilesDatabase)EditorGUILayout.ObjectField(database, typeof(SusFilesDatabase), false);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Target Extension:.sus");
            DrawFileDragArea(GUILayoutUtility.GetRect(0, 100, GUILayout.ExpandWidth(true)),
                "Drag & Drop .sus files here", _targetExtension, ProcessFiles);

            if (GUILayout.Button("Clear"))
            {
                _stringBuilder.Clear();
                _filePaths.Clear();
            }

            if (GUILayout.Button("Convert"))
            {
                ConvertFiles();
            }

            // スクロールビューの開始
            _scrollPosition =
                EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.Height(100), GUILayout.ExpandWidth(true));
            // スクロール内でLabelを使用してテキスト表示。GUIStyleを使ってスタイルを適用
            EditorGUILayout.LabelField(_stringBuilder.ToString(),
                new GUIStyle { richText = true, wordWrap = true, normal = { textColor = Color.white } },
                GUILayout.ExpandHeight(true));
            EditorGUILayout.EndScrollView(); // スクロールビューの終了
        }

        private void DrawFileDragArea(Rect dropArea, string dropAreaMessage, string targetFileExtension,
            UnityAction<string[]> dropCallback)
        {
            GUI.Box(dropArea, dropAreaMessage);
            var evt = Event.current;

            if (!dropArea.Contains(evt.mousePosition))
                return;

            if (evt.type == EventType.DragUpdated)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
            }
            else if (evt.type == EventType.DragPerform)
            {
                DragAndDrop.AcceptDrag();

                List<string> validPaths = new List<string>();
                foreach (string path in DragAndDrop.paths)
                {
                    if (Path.GetExtension(path).Equals(targetFileExtension, StringComparison.OrdinalIgnoreCase))
                    {
                        validPaths.Add(path);
                    }
                }

                dropCallback(validPaths.ToArray());
                Event.current.Use();
            }
        }

        private void ProcessFiles(string[] paths)
        {
            _filePaths.Clear();
            _stringBuilder.Clear();
            foreach (string path in paths)
            {
                _filePaths.Add(path);
                _stringBuilder.AppendLine(Path.GetFileName(path));
            }
        }

        private void ConvertFiles()
        {
            if (database == null)
            {
                Debug.LogError("No database selected!");
                return;
            }

            foreach (var filePath in _filePaths)
            {
                SusToJSONConverter converter = new SusToJSONConverter();
                try
                {
                    string json = converter.ConvertSusFileToJson(filePath);
                    string outputFileName = Path.ChangeExtension(filePath, ".json");
                    File.WriteAllText(outputFileName, json);
                    Debug.Log($"Converted successfully: {outputFileName}");

                    // データベースにファイルパスを記録
                    UpdateDatabase(filePath, outputFileName);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to convert {filePath}. Error: {e.Message}");
                }
            }

            // データベースの変更を保存
            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
        }

        private void UpdateDatabase(string susFilePath, string jsonFilePath)
        {
            bool entryExists = false;
            foreach (var entry in database.files)
            {
                if (entry.susFilePath != susFilePath) continue;
                entry.jsonFilePath = jsonFilePath;
                entry.isConverted = true;
                entryExists = true;
                break;
            }

            if (!entryExists)
            {
                List<SusFileEntry> fileList = new List<SusFileEntry>(database.files ?? new SusFileEntry[0]);
                fileList.Add(new SusFileEntry
                    { susFilePath = susFilePath, jsonFilePath = jsonFilePath, isConverted = true });
                database.files = fileList.ToArray();
            }
        }
    }
}