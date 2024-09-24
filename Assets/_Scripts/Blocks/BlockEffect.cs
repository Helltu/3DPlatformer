using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts
{
    public abstract class BlockEffect : MonoBehaviour
    {
        public virtual void ImplementEffectOnCharacter(GameObject character) { }
        public virtual void RemoveEffectFromCharacter(GameObject character) { }
    }
}

