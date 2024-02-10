using System.Collections;
using System.Collections.Generic;
using ChronicleDimensionProject.Boot;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public void Change(string sceneName)
    {
        SceneController.Instance.LoadScene(sceneName);
    }
}
