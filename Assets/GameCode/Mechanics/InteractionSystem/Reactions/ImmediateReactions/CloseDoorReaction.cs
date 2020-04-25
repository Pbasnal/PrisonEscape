using Assets.GameCode.SpelunkyLevelGen.LevelObjects;
using GameCode.InteractionSystem;

namespace GameCode.Mechanics.InteractionSystem.Reactions.ImmediateReactions
{
    public class CloseDoorReaction : Reaction
    {
        public Door doorToClose;

        protected override void ImmediateReaction()
        {
            if (doorToClose == null)
            {
                return;
            }

            doorToClose.CloseDoor();
        }
    }
}
