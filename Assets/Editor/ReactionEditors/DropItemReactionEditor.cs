using LockdownGames.Mechanics.InteractionSystem.Reactions.ImmediateReactions;

using UnityEditor;

namespace LockdownGames.EditorScripts.ReactionEditors
{
    [CustomEditor(typeof(DropItemReaction))]
    public class DropItemReactionEditor : ReactionEditor
    {
        protected override string GetFoldoutLabel()
        {
            return "Drop Item Reaction";
        }
    }

}
