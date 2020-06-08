using LockdownGames.Mechanics.InteractionSystem;

using UnityEngine;

namespace LockdownGames.Mechanics.ActorMechanics
{
    public class InteractionMechanics : MonoBehaviour, ICanInteract
    {
        public Interactable Interactable { get; private set; }

        public bool isInteractionPossible = false;

        public void SetInteractable(Interactable interactable)
        {
            if (Interactable != null)
            {
                Interactable.material.SetFloat("_OutlineThickness", 0);
            }

            Interactable = interactable;
            Interactable.material.SetFloat("_OutlineThickness", 1);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            GotHitByAnObject(collider);
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            HitEnded(collider);
        }

        private void OnTriggerStay2D(Collider2D collider)
        {
            GotHitByAnObject(collider);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            GotHitByAnObject(collision.collider);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            HitEnded(collision.collider);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            GotHitByAnObject(collision.collider);
        }

        private bool ShouldInteract(Collider2D collider)
        {
            if (collider == null)
            {
                return false;
            }

            var interactableComponent = collider.GetComponent<Interactable>();
            if (interactableComponent == null
                || Interactable == null
                || Interactable.name != interactableComponent.name)
            {
                return false;
            }

            return true;
        }

        private void HitEnded(Collider2D collider)
        {
            if (ShouldInteract(collider) == false)
            {
                return;
            }

            isInteractionPossible = false;
            Interactable = null;
        }

        private void GotHitByAnObject(Collider2D collider)
        {
            if (ShouldInteract(collider) == false)
            {
                return;
            }

            isInteractionPossible = true;
            Interactable.material.SetFloat("_OutlineThickness", 0);
            Interactable.Interact(this);
            Interactable = null;
        }
    }
}
