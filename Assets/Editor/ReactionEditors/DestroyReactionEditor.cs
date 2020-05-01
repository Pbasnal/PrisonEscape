using LockdownGames.GameCode.Mechanics.InteractionSystem.Mechanics.Reactions.ImmediateReactions;

using UnityEditor;

namespace LockdownGames.EditorScripts.ReactionEditors
{
    [CustomEditor(typeof(DestroyReaction))]
    public class DestroyReactionEditor : ReactionEditor
    {
        protected override string GetFoldoutLabel()
        {
            return "Destroy Reaction";
        }
    }

}
