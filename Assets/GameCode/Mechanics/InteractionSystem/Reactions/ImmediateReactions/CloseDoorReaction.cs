using GameCode.InteractionSystem;
using UnityEngine;

namespace GameCode.Mechanics.InteractionSystem.Reactions.ImmediateReactions
{
    public class CloseDoorReaction : Reaction
    {
        public override void React(MonoBehaviour monoBehaviour)
        {
            Debug.Log(monoBehaviour.name);
            ImmediateReaction();
        }

        protected override void ImmediateReaction()
        {
            Debug.Log("Door is closed");
        }
    }
}
