

using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

namespace Game.Jam
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public TextMeshProUGUI GameStatusText;
        public TextMeshProUGUI PlayerStatusText;
        public TextMeshProUGUI PlayerCountText;
        public GameObject StartButton;
        public string GameVersion = "1.0";

        public int MaxPlayers = 2;

        void Awake()
        {
            //4 
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        // Start is called before the first frame update
        void Start()
        {
            GameStatusText.text = "";

            StartButton.SetActive(false);

            ConnectToPhoton();
        }

        public void ConnectToPhoton()
        {
            GameStatusText.text = "Connecting...";
            PhotonNetwork.GameVersion = GameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }

        public void JoinRoom()
        {
            if (PhotonNetwork.IsConnected)
            {
                StartButton.SetActive(false);
                GameStatusText.text = "Searching for room...";
                PhotonNetwork.JoinRandomRoom();
            }
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 2;

            PhotonNetwork.CreateRoom(null, roomOptions);
        }

        public void StartGame()
        {
            // 5
            if (PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayers)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.LoadLevel("Multiplayer");
                }
            }
            else
            {
                GameStatusText.text = $"Minimum {MaxPlayers} Players required to Start!";
            }
        }

        // Photon Methods
        public override void OnConnected()
        {
            // 1
            base.OnConnected();
            // 2
            GameStatusText.text = "Connected to Servers!";
            GameStatusText.color = Color.green;
            StartButton.SetActive(true);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogError("Disconnected. Please check your Internet connection.");
            GameStatusText.text = "Disconnected. Please check your Internet connection.";
        }
        private void RefreshRoomStats()
        {
            PlayerCountText.text = $"{PhotonNetwork.CurrentRoom.PlayerCount }/{MaxPlayers} Players";
            GameStatusText.text = "Waiting for Players";

            if (PhotonNetwork.IsMasterClient)
            {
                PlayerStatusText.text = "Leader";
            }
            else
            {
                PlayerStatusText.text = "Member";
            }

            if (PhotonNetwork.CurrentRoom.PlayerCount >= MaxPlayers)
            {
                GameStatusText.text = "Starting";
                StartGame();
            }
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            RefreshRoomStats();
        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player newPlayer)
        {
            base.OnPlayerLeftRoom(newPlayer);
            RefreshRoomStats();
        }


        public override void OnJoinedRoom()
        {
            RefreshRoomStats();
        }
    }
}