using System;
using System.Collections;
using UnityEngine;

namespace _Scripts
{
    public class CloudBlock : BlockEffect
    {
        private bool continueAction;
        private bool activated;

        public override void ImplementEffectOnCharacter(GameObject character)
        {
            continueAction = true;
            if (!activated)
                StartCoroutine(CloudBlockCoroutine());
        }

        public override void RemoveEffectFromCharacter(GameObject character)
        {
            continueAction = false;
        }

        private IEnumerator CloudBlockCoroutine()
        {
            activated = true;
            while (continueAction)
            {
                yield return new WaitForSeconds(1f);
                Vector3 end = new Vector3(transform.position.x, transform.position.y - 6, transform.position.z);
                while (transform.position != end)
                {
                    transform.position = Vector3.MoveTowards(transform.position, end, 3f * Time.deltaTime);
                    yield return new WaitForEndOfFrame ();
                }

                yield return new WaitForSeconds(5f);

                end = new Vector3(transform.position.x, transform.position.y + 6, transform.position.z);
                while (transform.position != end)
                {
                    transform.position = Vector3.MoveTowards(transform.position, end, 3f * Time.deltaTime);
                    yield return new WaitForEndOfFrame ();
                }
            }

            activated = false;
            yield return null;
        }
    }
}