using GameCode.InteractionSystem.Mechanics;
using GameCode.Mechanics.InteractionSystem.Mechanics;
using UnityEngine;

namespace GameCode.Mechanics.PlayerMechanics
{
    public class InteractionMechanics : MonoBehaviour, ICanInteract
    {
        public Interactable Interactable { get; private set; }

        public bool isInteractionPossible = false;

        public void SetInteractable(Interactable interactable)
        {
            Interactable = interactable;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider == null)
            {
                return;
            }

            var interactable = collision.collider.GetComponent<Interactable>();
            if (interactable == null)
            {
                return;
            }

            isInteractionPossible = true;
            interactable.Interact();
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider == null)
            {
                return;
            }

            var interactable = collision.collider.GetComponent<Interactable>();
            if (interactable == null)
            {
                return;
            }

            isInteractionPossible = false;
        }
    }
}
