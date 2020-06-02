using LockdownGames.Mechanics.InteractionSystem.Reactions.ImmediateReactions;

using UnityEditor;

namespace LockdownGames.EditorScripts.ReactionEditors
{
    [CustomEditor(typeof(ElevatorDoorReaction))]
    public class ElevatorDoorReactionEditor : ReactionEditor
    {
        protected override string GetFoldoutLabel()
        {
            return "Elevator Door Reaction";
        }
    }

}
