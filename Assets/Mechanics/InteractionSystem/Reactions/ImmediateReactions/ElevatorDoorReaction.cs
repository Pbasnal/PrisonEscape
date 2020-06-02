using UnityEngine;

namespace LockdownGames.Mechanics.InteractionSystem.Reactions.ImmediateReactions
{
    public class ElevatorDoorReaction : Reaction
    {
        protected override void ImmediateReaction()
        {
            Debug.Log("Elevator door triggered");
        }
    }
}
