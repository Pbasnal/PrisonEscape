using Assets.GameCode.SpelunkyLevelGen.LevelObjects;
using GameCode.InteractionSystem;
using UnityEngine;

namespace GameCode.Mechanics.InteractionSystem.Reactions.ImmediateReactions
{
    public class OpenDoorReaction : Reaction
    {
        public override void React(MonoBehaviour monoBehaviour)
        {
            base.React(monoBehaviour);

            var door = monoBehaviour.GetComponent<Door>();

            if (door == null)
            {
                throw new UnityException("Door behaviour is not set");
            }

            door.OpenDoor();
            //Debug.Log("Open door triggered");
        }

        protected override void ImmediateReaction()
        {}
    }
}
