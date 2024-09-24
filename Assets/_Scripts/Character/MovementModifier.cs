using System;
using UnityEngine;

namespace _Scripts
{
    public class MovementModifier : MonoBehaviour
    {
        private Rigidbody rb;

        private VectorContainer movementModificatorContainer = new VectorContainer(Vector3.zero);
        private Vector3 inAirMovement = Vector3.zero;

        public VectorContainer MovementModificatorContainer
        {
            get => this.movementModificatorContainer;
        }

        public Vector3 InAirMovement
        {
            get => this.inAirMovement;
        }

        public void SetMovementModificator(VectorContainer vectorContainer)
        {
            movementModificatorContainer = vectorContainer;
        }

        public void SetInAirMovement(Vector3 inAirMovement)
        {
            this.inAirMovement = inAirMovement;
        }
    }
}