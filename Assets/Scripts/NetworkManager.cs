

using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Game.Jam
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public ButtonTextColor ForceStart;
        public WaitingText GameStatusText;
        public string GameVersion = "1.0";
        public byte MaxPlayers = 2;


        void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        void Start()
        {
            ForceStart.SetEnabled(false);
            ConnectToPhoton();
        }


        private void ConnectToPhoton()
        {
            if (!PhotonNetwork.IsConnected)
            {
                GameStatusText.SetText("Connecting", true);
                PhotonNetwork.GameVersion = GameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
            else
            {
                OnConnectedToMaster();
            }
        }

        private void JoinRoom()
        {
            if (PhotonNetwork.IsConnected)
            {
                GameStatusText.SetText("Searching for room", true);
                PhotonNetwork.JoinRandomRoom();
            }
        }

        public void TryStarting()
        {

            if (PhotonNetwork.CurrentRoom.PlayerCount >= MaxPlayers)
            {
                GameStatusText.SetText("Starting", true);
                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.LoadLevel("Instructions");
                }
            }
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Room Joined");
            GameStatusText.SetText("Waiting for Players", true);
            TryStarting();


        }
        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            TryStarting();
        }
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = MaxPlayers;

            PhotonNetwork.CreateRoom(null, roomOptions);
        }

        public void StartSolo()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("Instructions");
            }
        }

        // Photon Methods
        public override void OnConnectedToMaster()
        {
            base.OnConnected();
            GameStatusText.SetText("Joining Room", true);
            ForceStart.SetEnabled(true);
            JoinRoom();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            GameStatusText.SetText("Disconnected. Please check your Internet connection.", false);
        }


    }
}