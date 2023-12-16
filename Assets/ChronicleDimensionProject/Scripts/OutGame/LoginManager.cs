using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.IO;
using ChronicleDimensionProject.Player;
using Cysharp.Threading.Tasks;


namespace ChronicleDimensionProject
{
    public class LoginManager : MonoBehaviour
    {
        private const string FilePath = "userAccount.json";

        public static async UniTask<bool> LoginAsync(string userID, string password)
        {
            string path = Application.persistentDataPath + "/" + FilePath;

            if (File.Exists(path))
            {
                string json = await File.ReadAllTextAsync(path);
                UserAccount account = JsonUtility.FromJson<UserAccount>(json);

                return account.userID == userID && account.password == password;
            }

            return false;
        }
    }
}