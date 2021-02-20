using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Jam
{
    public class Countdown : MonoBehaviour
    {


        public delegate void TimesUpHandler();
        public event TimesUpHandler TimesUp;

        public Image LoadingBar;
        public TextMeshProUGUI TextIndicator;
        public float StartTimer = 5;
        private float timeRemaining;
        private int lastSecond;
        private bool isActive = false;

        public void Awake()
        {
            timeRemaining = StartTimer;
            lastSecond = (int)Math.Round(timeRemaining, 0);
            LoadingBar.fillAmount = 1;
            TextIndicator.text = "" + lastSecond;
        }
        public void BeginCountdown()
        {
            isActive = true;
        }


        void Update()
        {

            if (isActive && timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                LoadingBar.GetComponent<Image>().fillAmount = timeRemaining / StartTimer;
                if ((int)timeRemaining != lastSecond)
                {
                    lastSecond = (int)Math.Round(timeRemaining, 0);
                    TextIndicator.text = "" + lastSecond;

                }
            }
            else if (isActive)
            {
                isActive = false;
                LoadingBar.GetComponent<Image>().fillAmount = 0;

                TimesUp?.Invoke();
            }
        }
    }
}
