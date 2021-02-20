using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Jam
{
    public class MenuManager : MonoBehaviour
    {
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
