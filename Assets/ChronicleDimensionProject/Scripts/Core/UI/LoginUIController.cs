using ChronicleDimensionProject.Scripts.OutGame;
using UnityEngine;
using UnityEngine.UI;

namespace ChronicleDimensionProject.Scripts.Core.UI
{
    public class UIController : MonoBehaviour
    {
        public InputField userIDField;
        public InputField passwordField;

        public async void OnLoginButtonClicked()
        {
            bool loginSuccess = await LoginManager.LoginAsync(userIDField.text, passwordField.text);
            if (loginSuccess)
            {
                // ログイン成功の処理
                Debug.Log($"ログイン成功 : アカウント名{userIDField.text}");
            }
            else
            {
                // ログイン失敗の処理
                Debug.Log("ログイン失敗");
            }
        }

        public void OnCreateAccountButtonClicked()
        {
            AccountManager.CreateAccount(userIDField.text, passwordField.text);
            // アカウント作成成功の処理
        }
    }
}