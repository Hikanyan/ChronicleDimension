using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

namespace ChronicleDimensionProject
{
    public class LogInManager : MonoBehaviourPunCallbacks, IChatClientListener
    {
        private string playFabId;
        private ChatClient chatClient;
        public InputField messageInputField;
        public Text chatDisplayText;
        private const string ChatAppId = "YOUR_PHOTON_CHAT_APP_ID"; // Photon ChatのAppID

        void Start()
        {
            // PlayFabの初期化とログイン
            var request = new LoginWithCustomIDRequest
                { CustomId = SystemInfo.deviceUniqueIdentifier, CreateAccount = true };
            PlayFabClientAPI.LoginWithCustomID(request, OnPlayFabLoginSuccess, OnPlayFabLoginFailure);
        }

        private void OnPlayFabLoginSuccess(LoginResult result)
        {
            playFabId = result.PlayFabId;
            Debug.Log("PlayFab login successful. PlayFabId: " + playFabId);

            // Photon Realtimeの初期化
            PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime = "YOUR_PHOTON_REALTIME_APP_ID";
            PhotonNetwork.ConnectUsingSettings();

            // Photon Chatの初期化
            chatClient = new ChatClient(this);
            chatClient.Connect(ChatAppId, "1.0", new Photon.Chat.AuthenticationValues(playFabId));
        }

        private void OnPlayFabLoginFailure(PlayFabError error)
        {
            Debug.LogError("PlayFab login failed: " + error.GenerateErrorReport());
        }

        public override void OnConnectedToMaster()
        {
            // プレイヤーをランダムなルームに参加させる
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            // ルームが見つからなかった場合、新しいルームを作成
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Joined room successfully.");
        }

        // IChatClientListenerの実装
        public void DebugReturn(DebugLevel level, string message)
        {
            Debug.Log("Chat DebugReturn: " + message);
        }

        public void OnChatStateChange(ChatState state)
        {
            Debug.Log("Chat state changed: " + state);
        }

        public void OnConnected()
        {
            Debug.Log("Chat connected.");
            chatClient.Subscribe(new string[] { "global" });
        }

        public void OnDisconnected()
        {
            Debug.Log("Chat disconnected.");
        }

        public void OnGetMessages(string channelName, string[] senders, object[] messages)
        {
            for (int i = 0; i < messages.Length; i++)
            {
                Debug.Log($"[{channelName}] {senders[i]}: {messages[i]}");
                chatDisplayText.text += $"{senders[i]}: {messages[i]}\n";
            }
        }

        public void OnPrivateMessage(string sender, object message, string channelName)
        {
            Debug.Log($"Private message from {sender}: {message}");
        }

        public void OnSubscribed(string[] channels, bool[] results)
        {
            Debug.Log("Subscribed to channels: " + string.Join(", ", channels));
        }

        public void OnUnsubscribed(string[] channels)
        {
            Debug.Log("Unsubscribed from channels: " + string.Join(", ", channels));
        }

        public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
        {
            Debug.Log($"Status update from {user}: {status}");
        }

        public void OnUserSubscribed(string channel, string user)
        {
            Debug.Log($"User {user} subscribed to channel {channel}");
        }

        public void OnUserUnsubscribed(string channel, string user)
        {
            Debug.Log($"User {user} unsubscribed from channel {channel}");
        }

        void Update()
        {
            chatClient.Service(); // 必要なフレームごとの呼び出し
        }

        public void SendMessage()
        {
            string message = messageInputField.text;
            if (!string.IsNullOrEmpty(message))
            {
                chatClient.PublishMessage("global", message);
                messageInputField.text = string.Empty;
            }
        }
    }
}