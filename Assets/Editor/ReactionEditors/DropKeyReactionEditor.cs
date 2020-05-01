using LockdownGames.GameCode.Mechanics.InteractionSystem.Mechanics.Reactions.ImmediateReactions;

using UnityEditor;

namespace LockdownGames.EditorScripts.ReactionEditors
{
    [CustomEditor(typeof(DropKeyReaction))]
    public class DropKeyReactionEditor : ReactionEditor
    {
        protected override string GetFoldoutLabel()
        {
            return "Drop Key Reaction";
        }
    }

}
