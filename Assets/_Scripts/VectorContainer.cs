using UnityEngine;

namespace _Scripts
{
    public class VectorContainer
    {
        private Vector3 innerVector;
        public Vector3 InnerVector { get; set; }

        public VectorContainer(Vector3 innerVector)
        {
            this.innerVector = innerVector;
        }

        public VectorContainer()
        {
            
        }
    }
}