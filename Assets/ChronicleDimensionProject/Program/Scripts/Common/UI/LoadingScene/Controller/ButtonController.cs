using UnityEngine;
using UnityEngine.UI;

namespace ChronicleDimensionProject.Common.UI
{
    public class ButtonController : MonoBehaviour
    {
        public void Change(string sceneName)
        {
            LoadingScene.Instance.LoadNextScene(sceneName);
        }

        public void SetActive(GameObject setActiveObject)
        {
            setActiveObject.SetActive(!setActiveObject.activeSelf);
        }
    }
}