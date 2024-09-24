using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts
{
    public class RespawnManager : MonoBehaviour
    {
        [SerializeField] private GameObject playerContainerGameObject;
        [SerializeField] private GameObject playerGameObject;
        public event EventHandler<EventArgs> OnRespawnEvent;

        public void RespawnPlayer()
        {
            if (playerContainerGameObject.activeSelf)
                return;
            
            playerGameObject.transform.position = new Vector3(-1.5f, 0, 1.5f);
            playerContainerGameObject.SetActive(true);
            playerGameObject.GetComponent<CharacterHpManager>().SetMaxHp();
            OnRespawnEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}