using LockdownGames.Mechanics.InteractionSystem.Reactions.ImmediateReactions;

using UnityEditor;

namespace LockdownGames.EditorScripts.ReactionEditors
{
    [CustomEditor(typeof(ToggleDoorReaction))]
    public class ToggleDoorReactionEditor : ReactionEditor
    {
        protected override string GetFoldoutLabel()
        {
            return "Toggle Door Reaction";
        }
    }

}
