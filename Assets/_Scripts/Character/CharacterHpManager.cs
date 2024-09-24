using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class CharacterHpManager : MonoBehaviour
    {
        [SerializeField] private short currentHp;
        [SerializeField] private Image healthBar;

        public short CurrentHp
        {
            get => currentHp;
            set
            {
                healthBar.fillAmount = (float) value / GetMaxHp();
                currentHp = value;
            }
        }

        public event EventHandler<EventArgs> OnDeathEvent;

        private void Awake()
        {
            SetMaxHp();
        }

        private void Update()
        {
            if (currentHp > 0)
                return;

            OnDeathEvent?.Invoke(this, EventArgs.Empty);
            transform.parent.gameObject.SetActive(false);
        }
        
        public void SetMaxHp()
        {
            CurrentHp = 100;
        }
        
        private short GetMaxHp()
        {
            return 100;
        }

    }
}