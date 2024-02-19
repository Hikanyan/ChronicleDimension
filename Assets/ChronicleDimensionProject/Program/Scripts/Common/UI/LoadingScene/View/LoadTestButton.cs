using UnityEngine;

namespace ChronicleDimensionProject.Common.UI
{
    public class LoadTestButton : MonoBehaviour
    {
        
        public LoadingSceneView loadingScene;

        public void OnButtonClick()
        {
            loadingScene.LoadScene("LoadingScene1");
        }
        public void LoadScene(string sceneName)
        {
            LoadingScene.Instance.LoadNextScene(sceneName);
        }
        
    }
}