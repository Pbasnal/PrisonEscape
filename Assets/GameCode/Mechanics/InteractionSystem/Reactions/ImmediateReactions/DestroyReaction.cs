using GameCode.Interfaces;
using UnityEngine;

namespace GameCode.InteractionSystem.Reactions.ImmediateReactions
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
