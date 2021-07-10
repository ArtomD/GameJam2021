using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Jam
{
    public class MenuManager : MonoBehaviour
    {

        private float gameOverDelay = 1.0f;
        public void LoadMenu()
        {
            SceneManager.LoadScene("Start");
        }

        public void LoadLobby()
        {
            SceneManager.LoadScene("Search");
        }

        public void LoadOptions()
        {
            SceneManager.LoadScene("Options");
        }
    }

}
