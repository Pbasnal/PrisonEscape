using GameCode.InteractionSystem.Reactions.ImmediateReactions;
using UnityEditor;

namespace EditorScripts.ReactionEditors
{
    [CustomEditor(typeof(PickUpItemReaction))]
    public class PickedUpItemReactionEditor : ReactionEditor
    {
        protected override string GetFoldoutLabel()
        {
            return "Pick Up Item Reaction";
        }
    }
}
