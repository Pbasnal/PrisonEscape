using UnityEngine;

namespace LockdownGames.GameCode.Mechanics.InteractionSystem.Mechanics.Reactions.ImmediateReactions
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
