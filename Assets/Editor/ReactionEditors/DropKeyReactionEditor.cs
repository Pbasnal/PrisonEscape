using GameCode.InteractionSystem.Reactions.ImmediateReactions;
using UnityEditor;

namespace EditorScripts.ReactionEditors
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
