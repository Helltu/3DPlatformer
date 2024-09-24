using System;
using System.Collections;
using UnityEngine;

namespace _Scripts
{
    public class LavaBlock : BlockEffect
    {
        private CharacterHpManager charSteppedOnCharacterHpManager;
        private MeshRenderer rr;
        [SerializeField] private byte damage = 50;
        [SerializeField] private Material baseMaterial;
        [SerializeField] private Material lava;
        [SerializeField] private Material hurtRed;

        private bool continueAction;
        private bool activated;

        private void Awake()
        {
            rr = GetComponent<MeshRenderer>();
        }

        public override void ImplementEffectOnCharacter(GameObject character)
        {
            charSteppedOnCharacterHpManager = character.GetComponent<CharacterHpManager>();
            charSteppedOnCharacterHpManager.OnDeathEvent += CharSteppedOnCharacterHpManagerOnDeathEvent;
            continueAction = true;
            if (!activated)
                StartCoroutine(LavaBlockCoroutine());
        }
        
        public override void RemoveEffectFromCharacter(GameObject character)
        {
            charSteppedOnCharacterHpManager.OnDeathEvent -= CharSteppedOnCharacterHpManagerOnDeathEvent;
            charSteppedOnCharacterHpManager = null;
            continueAction = false;
        }
        
        private void CharSteppedOnCharacterHpManagerOnDeathEvent(object sender, EventArgs e)
        {
            RemoveEffectFromCharacter(sender as GameObject);
        }


        private IEnumerator LavaBlockCoroutine()
        {
            activated = true;
            while (continueAction)
            {
                var materials = rr.materials;
                materials[1] = lava;
                rr.materials = materials;
                yield return new WaitForSeconds(1f);
                materials = rr.materials;
                materials[1] = hurtRed;
                rr.materials = materials;
                
                if(charSteppedOnCharacterHpManager != null)
                    charSteppedOnCharacterHpManager.CurrentHp -= damage;
                
                yield return new WaitForSeconds(0.3f);
                materials = rr.materials;
                materials[1] = baseMaterial;
                rr.materials = materials;
                yield return new WaitForSeconds(5f);
            }
            
            activated = false;
            yield return null;
        }
    }
}