using System.IO;
using UnityEditor;

namespace HikanyanLaboratory.Editor
{
    public class ProjectStructureGenerator
    {
        public void CreateProjectFolders(string projectName)
        {
            string rootPath = $"Assets/{projectName}";
            CreateFolder(rootPath);
            CreateFolder($"{rootPath}/Design");
            CreateFolder($"{rootPath}/GameData");
            CreateFolder($"{rootPath}/GameData/Audio");
            CreateFolder($"{rootPath}/GameData/Prefabs");
            CreateFolder($"{rootPath}/GameData/Prefabs/Audio");
            CreateFolder($"{rootPath}/GameData/Prefabs/UI");
            CreateFolder($"{rootPath}/GameData/Prefabs/UI/Templates");
            CreateFolder($"{rootPath}/GameData/Prefabs/GamePlay");
            CreateFolder($"{rootPath}/GameData/ScriptableObject");
            CreateFolder($"{rootPath}/GameData/ScriptableObject/Audio");
            CreateFolder($"{rootPath}/Scenes");
            CreateFolder($"{rootPath}/Scenes/Debug");
            CreateFolder($"{rootPath}/Scenes/Main");
            CreateFolder($"{rootPath}/Scenes/Main/Title");
            CreateFolder($"{rootPath}/Scenes/Main/InGame");
            CreateFolder($"{rootPath}/Scenes/Main/Result");
            CreateFolder($"{rootPath}/Scripts");
            CreateFolder($"{rootPath}/Scripts/Editor");
            CreateFolder($"{rootPath}/Scripts/InGame");
            CreateFolder($"{rootPath}/Scripts/Result");
            CreateFolder($"{rootPath}/Scripts/Shader");
            CreateFolder($"{rootPath}/Scripts/System");
            CreateFolder($"{rootPath}/Scripts/System/AudioManager");
            CreateFolder($"{rootPath}/Scripts/System/SceneManager");
            CreateFolder($"{rootPath}/Scripts/UI");
            CreateFolder($"{rootPath}/Scripts/UI/UIManager");
            CreateFolder($"{rootPath}/Scripts/UI/UIManager/Editor");
            CreateFolder($"{rootPath}/Scripts/UI/UIManager/Node");
            CreateFolder($"{rootPath}/Scripts/UI/InGame");
            CreateFolder($"{rootPath}/Scripts/UI/Title");
            CreateFolder($"{rootPath}/Scripts/UI/Result");
            CreateFolder($"{rootPath}/Scripts/Title");
            CreateFolder($"{rootPath}/Settings");
            CreateFolder("Assets/Plugins");
            CreateFolder("Assets/Resources");
            CreateFolder("Assets/AssetStoreTools");
            CreateFolder("Assets/StreamingAssets");
        }

        private void CreateFolder(string path)
        {
            if (!AssetDatabase.IsValidFolder(path))
            {
                string parentFolder = Path.GetDirectoryName(path);
                string newFolderName = Path.GetFileName(path);

                if (!AssetDatabase.IsValidFolder(parentFolder))
                {
                    CreateFolder(parentFolder); // 上の階層が存在しない場合は再帰的に作成
                }

                AssetDatabase.CreateFolder(parentFolder, newFolderName);
            }
        }
    }
}
