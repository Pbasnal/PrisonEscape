using UnityEngine;

namespace LockdownGames.Mechanics.InteractionSystem.Reactions.ImmediateReactions
{
    public class DestroyReaction : Reaction
    {
        public GameObject reactionInteractable;

        protected override void ImmediateReaction()
        {
            Destroy(reactionInteractable);
        }
    }
}
