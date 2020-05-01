using LockdownGames.Mechanics.InteractionSystem.Reactions.ImmediateReactions;

using UnityEditor;

namespace LockdownGames.EditorScripts.ReactionEditors
{
    [CustomEditor(typeof(PickUpItemReaction))]
    public class PickUpItemReactionEditor : ReactionEditor
    {
        protected override string GetFoldoutLabel()
        {
            return "Pick Up Item Reaction";
        }
    }
}
