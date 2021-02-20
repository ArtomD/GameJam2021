using UnityEngine;
using UnityEngine.SceneManagement;


using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.PunBasics;

namespace Game.Jam
{
    public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
    {

        private GameObject P1;
        private GameObject P2;
        public GameObject Player1SpawnPosition;
        public GameObject Player2SpawnPosition;


        private Camera camera;
        void Start()
        {
            if (!PhotonNetwork.IsConnected) // 1
            {
                SceneManager.LoadScene(0); return;
            }

            if (PlayerManager.LocalPlayerInstance == null)
            {
                if (PhotonNetwork.IsMasterClient) // 2
                {
                    Debug.Log("Instantiating Player 1");
                    // 3
                    P1 = PhotonNetwork.Instantiate("Ball", Player1SpawnPosition.transform.position, Player1SpawnPosition.transform.rotation, 0);

                }
                else // 5
                {
                    Debug.Log("Instantiating Player 2");
                    P2 = PhotonNetwork.Instantiate("Camera", Player2SpawnPosition.transform.position, Player2SpawnPosition.transform.rotation, 0);
                    camera = FindObjectOfType<Camera>();
                }
            }
        }

        void Update()
        {
            // "back" button of phone equals "Escape". quit app if that's pressed
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitApplication();
            }
        }
        public void QuitApplication()
        {
            Application.Quit();
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            SceneManager.LoadScene(0);
        }
        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.Log("OnPlayerLeftRoom() " + other.NickName); // seen when other disconnects

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

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

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            throw new System.NotImplementedException();
        }
    }
}