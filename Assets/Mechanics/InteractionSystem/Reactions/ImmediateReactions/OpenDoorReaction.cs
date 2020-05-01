using LockdownGames.GameCode.SpelunkyLevelGen.LevelObjects;

namespace LockdownGames.Mechanics.InteractionSystem.Reactions.ImmediateReactions
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
