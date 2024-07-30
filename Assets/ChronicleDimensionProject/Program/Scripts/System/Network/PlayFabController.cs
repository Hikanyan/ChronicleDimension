using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace ChronicleDimensionProject.Network
{
    public class PlayFabController : MonoBehaviour
    {
        void Start()
        {
            PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest
                {
                    CustomId = SystemInfo.deviceUniqueIdentifier,
                    CreateAccount = true
                }
                ,
                result => { Debug.Log("ログイン成功"); },
                error => { Debug.Log("ログイン失敗"); });
        }
    }
}