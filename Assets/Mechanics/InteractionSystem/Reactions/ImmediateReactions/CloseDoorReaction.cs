using LockdownGames.GameCode.SpelunkyLevelGen.LevelObjects;

namespace LockdownGames.Mechanics.InteractionSystem.Reactions.ImmediateReactions
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
