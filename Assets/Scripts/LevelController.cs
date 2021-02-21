using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;


namespace Game.Jam
{
    public class LevelController : MonoBehaviour
    {

        [SerializeField] GameObject winPanel;
        [SerializeField] GameObject losePanel;
        [SerializeField] TextMeshProUGUI timeText;

        bool gameOver = false;

        void Update()
        {
            if (!gameOver)
                timeText.text = "Time:" + Math.Round(Time.timeSinceLevelLoad, 1);
        }


        public void Win()
        {
            gameOver = true;
            winPanel.SetActive(true);
        }

        public void Lose()
        {
            gameOver = true;
            losePanel.SetActive(true);
        }
    }
}
