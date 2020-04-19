using GameCode.Mechanics.InteractionSystem.Reactions.ImmediateReactions;
using UnityEditor;

namespace EditorScripts.ReactionEditors
{
    [CustomEditor(typeof(PickedUpKeyReaction))]
    public class PickedUpKeyReactionEditor : ReactionEditor
    {
        protected override string GetFoldoutLabel()
        {
            return "Pick Up key Reaction";
        }
    }
}
