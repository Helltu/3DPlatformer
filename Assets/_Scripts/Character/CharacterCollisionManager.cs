using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts
{
    public class CharacterCollisionManager : MonoBehaviour
    {
        [SerializeField] private TimerManager timerManager;
        private const byte GROUND_LAYER = 3;

        private byte groundCollisions = 0;

        public byte GroundCollisions
        {
            get => groundCollisions;
        }

        private void OnEnable()
        {
            groundCollisions = 0;
        }

        private void OnDisable()
        {
            groundCollisions = 0;
        }

        private void OnTriggerEnter(Collider triggerCollider)
        {
            if (triggerCollider.gameObject.layer == GROUND_LAYER)
                groundCollisions++;

            if (triggerCollider.CompareTag("BlockEffectTrigger"))
                triggerCollider.gameObject.GetComponentInParent<BlockEffect>().ImplementEffectOnCharacter(gameObject);


            if (triggerCollider.CompareTag("KillBox"))
                gameObject.GetComponent<CharacterHpManager>().CurrentHp = 0;


            if (triggerCollider.CompareTag("StartTimerTrigger") && timerManager.StartTime == DateTime.MinValue)
                timerManager.StartTimer();

            if (triggerCollider.CompareTag("StopTimerTrigger") && timerManager.StopTime == DateTime.MinValue)
                timerManager.StopTimer(true);
        }

        private void OnTriggerExit(Collider triggerCollider)
        {
            if (triggerCollider.CompareTag("BlockEffectTrigger"))
                triggerCollider.gameObject.GetComponentInParent<BlockEffect>().RemoveEffectFromCharacter(gameObject);

            if (triggerCollider.gameObject.layer == GROUND_LAYER)
                groundCollisions--;
        }
    }
}