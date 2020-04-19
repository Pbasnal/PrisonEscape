using GameCode.InteractionSystem.Reactions.ImmediateReactions;
using UnityEditor;

namespace EditorScripts.ReactionEditors
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
