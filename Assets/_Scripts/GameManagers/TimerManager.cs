using System;
using UnityEngine;

namespace _Scripts
{
    public class TimerManager : MonoBehaviour
    {
        [SerializeField] private CharacterHpManager characterHpManager;
        [SerializeField] private UIManager uiManager;
        private DateTime startTime;
        private DateTime stopTime;
        private String time = "0:0:000";

        public DateTime StartTime
        {
            get => startTime;
        }

        public DateTime StopTime
        {
            get => stopTime;
        }

        public String Time
        {
            get => time;
        }

        private void OnEnable()
        {
            characterHpManager.OnDeathEvent += CharacterHpManagerOnDeathEvent;
        }

        private void OnDisable()
        {
            characterHpManager.OnDeathEvent -= CharacterHpManagerOnDeathEvent;
        }

        private void CharacterHpManagerOnDeathEvent(object sender, EventArgs e)
        {
            StopTimer(false);
        }

        public void StartTimer()
        {
            startTime = DateTime.Now;
        }

        public void StopTimer(bool win)
        {
            characterHpManager.gameObject.transform.parent.gameObject.SetActive(false);
            if (startTime == DateTime.MinValue)
                time = "0:0:000";
            else
            {
                stopTime = DateTime.Now;
                TimeSpan deltaTime = stopTime - startTime;
                time = deltaTime.Minutes + ":" + deltaTime.Seconds + ":" +
                       deltaTime.Milliseconds;
            }

            startTime = DateTime.MinValue;
            stopTime = DateTime.MinValue;
            uiManager.ShowResults(win);
        }
    }
}