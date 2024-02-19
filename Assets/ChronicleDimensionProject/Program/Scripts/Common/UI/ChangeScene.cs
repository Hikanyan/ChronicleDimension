using ChronicleDimensionProject.Common.UI;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public void Change(string sceneName)
    {
        LoadingScene.Instance.LoadNextScene(sceneName);
    }
}
