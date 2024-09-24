using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Scripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private GameObject winLabel;
        [SerializeField] private GameObject looseLabel;
        [SerializeField] private TimerManager timerManager;
        [SerializeField] private CharacterHpManager characterHpManager;
        [SerializeField] private RespawnManager respawnManager;
        [SerializeField] private TMP_Text timeSpent;

        private void Awake()
        {
            Cursor.visible = false;
            restartButton.onClick.AddListener(delegate
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                winLabel.SetActive(false);
                looseLabel.SetActive(false);
                timeSpent.SetText("0:0:000");
                restartButton.gameObject.SetActive(false);
                respawnManager.RespawnPlayer();
            });
        }

        private void Update()
        {
            if (timerManager.StartTime == DateTime.MinValue)
                return;

            var deltaTime = DateTime.Now - timerManager.StartTime;
            var time = deltaTime.Minutes + ":" + deltaTime.Seconds + ":" +
                          deltaTime.Milliseconds;
            timeSpent.SetText(time);
        }

        private void OnEnable()
        {
            characterHpManager.OnDeathEvent += CharacterHpManagerOnOnDeathEvent;
        }

        private void OnDisable()
        {
            characterHpManager.OnDeathEvent -= CharacterHpManagerOnOnDeathEvent;
        }

        private void CharacterHpManagerOnOnDeathEvent(object sender, EventArgs e)
        {
            ShowResults(false);
        }

        public void ShowResults(bool win)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (win)
                winLabel.SetActive(true);
            else
                looseLabel.SetActive(true);
            timeSpent.SetText(timerManager.Time);
            restartButton.gameObject.SetActive(true);
        }
    }
}