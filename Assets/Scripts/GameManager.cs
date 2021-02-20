using UnityEngine;
using UnityEngine.SceneManagement;


using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.PunBasics;

namespace Game.Jam
{
    public class GameManager : MonoBehaviourPunCallbacks
    {

        private GameObject player1;
        private GameObject player2;
        public GameObject player1SpawnPosition;
        public GameObject player2SpawnPosition;
        void Start()
        {
            if (!PhotonNetwork.IsConnected) // 1
            {
                SceneManager.LoadScene("Launcher");
                return;
            }

            if (PlayerManager.LocalPlayerInstance == null)
            {
                if (PhotonNetwork.IsMasterClient) // 2
                {
                    Debug.Log("Instantiating Player 1");
                    // 3
                    player1 = PhotonNetwork.Instantiate("Ball", player1SpawnPosition.transform.position, player1SpawnPosition.transform.rotation, 0);

                }
                else // 5
                {
                    // Maybe spawn a blank object?
                    // player2 = PhotonNetwork.Instantiate("Car", player2SpawnPosition.transform.position, player2SpawnPosition.transform.rotation, 0);
                }
            }
        }

        public override void OnLeftRoom()
        {
            // Do something better
            SceneManager.LoadScene(0);
        }


        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }


    }
}