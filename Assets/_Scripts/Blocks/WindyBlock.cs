using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts
{
    public class WindyBlock : BlockEffect
    {
        private readonly List<Vector3> windDirections = new List<Vector3>{Vector3.forward, -Vector3.forward, Vector3.right, -Vector3.right};
        private Vector3 currentWindDirection;
        [SerializeField] private GameObject windDirectionArrow;

        private readonly VectorContainer currentWindContainer = new VectorContainer(Vector3.right);
        
        [SerializeField] private float windStrength = 0.05f; 
        
        public override void ImplementEffectOnCharacter(GameObject character)
        {
            character.GetComponent<MovementModifier>().SetMovementModificator(currentWindContainer);
        }

        public override void RemoveEffectFromCharacter(GameObject character)
        {
            character.GetComponent<MovementModifier>().SetMovementModificator(new VectorContainer(Vector3.zero));
        }

        private IEnumerator ChangeDirectionRoutine()
        {
            while (true)
            {
                var availableWindDirections = new List<Vector3>(windDirections);
                availableWindDirections.Remove(currentWindDirection);
                currentWindDirection = availableWindDirections[Random.Range(0, availableWindDirections.Count)];
                if (currentWindDirection == Vector3.right)
                    windDirectionArrow.transform.rotation = Quaternion.Euler(90, 0, 0);
                else if (currentWindDirection == Vector3.left)
                    windDirectionArrow.transform.rotation = Quaternion.Euler(90, 180, 0);
                else if (currentWindDirection == Vector3.forward)
                    windDirectionArrow.transform.rotation = Quaternion.Euler(90, -90, 0);
                else if (currentWindDirection == Vector3.back)
                    windDirectionArrow.transform.rotation = Quaternion.Euler(90, 90, 0);
                currentWindContainer.InnerVector = currentWindDirection * windStrength;
                yield return new WaitForSeconds(2f);
            }
        }

        private void Awake()
        {
            StartCoroutine(ChangeDirectionRoutine());
        }
    }
}


