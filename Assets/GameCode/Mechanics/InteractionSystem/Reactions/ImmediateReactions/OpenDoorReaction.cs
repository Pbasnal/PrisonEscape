using Assets.GameCode.SpelunkyLevelGen.LevelObjects;
using GameCode.InteractionSystem;

namespace GameCode.Mechanics.InteractionSystem.Reactions.ImmediateReactions
{
    public class OpenDoorReaction : Reaction
    {
        public Door doorToOpen;

        protected override void ImmediateReaction()
        {
            if (doorToOpen == null)
            {
                return;
            }

            doorToOpen.OpenDoor();
        }
    }
}
