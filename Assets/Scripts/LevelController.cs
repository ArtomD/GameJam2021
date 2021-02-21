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

        private Countdown exitCountdown;
        private GameManager gameManager;
        bool gameOver = false;

        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            exitCountdown = FindObjectOfType<Countdown>();
        }

        void OnEnable()
        {
            exitCountdown.TimesUp += ForceExit;
        }
        void OnDisable()
        {
            exitCountdown.TimesUp -= ForceExit;
        }

        void Update()
        {
            if (!gameOver)
                timeText.text = "Time:" + Math.Round(Time.timeSinceLevelLoad, 1);
        }


        public void Win()
        {
            gameOver = true;
            winPanel.SetActive(true);
            exitCountdown.BeginCountdown();

        }

        public void Lose()
        {
            gameOver = true;
            losePanel.SetActive(true);
            exitCountdown.BeginCountdown();
        }

        public void ForceExit()
        {
            gameManager.QuitToMenu();
        }
    }
}
