
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

namespace Game.Jam
{
    public class RoomManager : MonoBehaviour
    {
        public GameObject P1View;
        public GameObject P2View;

        public Countdown StartCountdown;


        private void OnEnable()
        {
            StartCountdown.TimesUp += LoadLevel;

        }

        private void OnDisable()
        {
            StartCountdown.TimesUp -= LoadLevel;

        }
        private void Start()
        {
            P1View.SetActive(false);
            P2View.SetActive(false);

            if (PhotonNetwork.IsMasterClient)
            {
                P1View.SetActive(true);
            }
            else
            {
                P2View.SetActive(true);

            }

            StartCountdown.BeginCountdown();
        }



        void LoadLevel()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("Game");
            }
        }


    }
}