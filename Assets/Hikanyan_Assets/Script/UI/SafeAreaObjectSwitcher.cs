using UnityEngine;

public class SafeAreaObjectSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject windowsObject;        // Windows���Ŏg�p����GameObject
    [SerializeField] private GameObject simulatorObject;      // Simulator���Ŏg�p����GameObject

    private void Start()
    {
        if (Application.isMobilePlatform)
        {
            // �Z�[�t�G���A�ɍ��킹��RectTransform�����Q�[���I�u�W�F�N�g���A�N�e�B�u�ɂ���
            simulatorObject.SetActive(true);
            windowsObject.SetActive(false);
        }
        else
        {
            // �ʏ��RectTransform�����Q�[���I�u�W�F�N�g���A�N�e�B�u�ɂ���
            simulatorObject.SetActive(false);
            windowsObject.SetActive(true);
        }
    }
}
