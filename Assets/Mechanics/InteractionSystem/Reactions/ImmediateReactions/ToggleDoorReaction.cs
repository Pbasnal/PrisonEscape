using LockdownGames.GameCode.SpelunkyLevelGen.LevelObjects;

namespace LockdownGames.Mechanics.InteractionSystem.Reactions.ImmediateReactions
{
    public class ToggleDoorReaction : Reaction
    {
        public Door doorToToggle;

        protected override void ImmediateReaction()
        {
            if (doorToToggle == null)
            {
                return;
            }

            if (doorToToggle.IsDoorClosed)
            {
                doorToToggle.OpenDoor();
            }
            else
            {
                doorToToggle.CloseDoor();
            }
        }
    }
}
