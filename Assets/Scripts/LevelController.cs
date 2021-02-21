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

        [SerializeField] private Countdown gameOverCountdown;
        [SerializeField] private Countdown victoryCountdown;
        private GameManager gameManager;
        bool gameOver = false;

        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        void OnEnable()
        {
            gameOverCountdown.TimesUp += ForceExit;
            victoryCountdown.TimesUp += ForceExit;
        }
        void OnDisable()
        {
            gameOverCountdown.TimesUp -= ForceExit;
            victoryCountdown.TimesUp -= ForceExit;
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
            victoryCountdown.BeginCountdown();

        }

        public void Lose()
        {
            gameOver = true;
            losePanel.SetActive(true);
            gameOverCountdown.BeginCountdown();
        }

        public void ForceExit()
        {
            gameManager.QuitToMenu();
        }
    }
}
