using System.IO;
using ChronicleDimensionProject.Player;
using UnityEngine;

namespace ChronicleDimensionProject.Scripts.OutGame
{
    public class AccountManager
    {
        private const string FilePath = "userAccount.json";

        public static void CreateAccount(string userID, string password)
        {
            UserAccount newAccount = new UserAccount { userID = userID, password = password };
            string json = JsonUtility.ToJson(newAccount);

            File.WriteAllText(Application.persistentDataPath + "/" + FilePath, json);
        }
    }
}