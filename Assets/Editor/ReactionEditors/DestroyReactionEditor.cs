using GameCode.InteractionSystem.Reactions.ImmediateReactions;
using UnityEditor;

namespace EditorScripts.ReactionEditors
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
