using UnityEditor;
using UnityEngine;

namespace HikanyanLaboratory.Editor
{
    public class ProjectStructureWindow : EditorWindow
    {
        private string _projectName = "MyProject";

        [MenuItem("HikanyanTools/Project Structure Generator")]
        public static void ShowWindow()
        {
            GetWindow<ProjectStructureWindow>("Project Structure Generator");
        }

        private void OnGUI()
        {
            GUILayout.Label("プロジェクト構造生成", EditorStyles.boldLabel);

            _projectName = EditorGUILayout.TextField("プロジェクト名", _projectName);

            if (GUILayout.Button("フォルダを生成"))
            {
                ProjectStructureGenerator generator = new ProjectStructureGenerator();
                generator.CreateProjectFolders(_projectName);
                Debug.Log("プロジェクトフォルダが生成されました。");
            }
        }
    }
}
