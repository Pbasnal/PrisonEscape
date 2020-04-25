using GameCode.Mechanics.InteractionSystem.Reactions.ImmediateReactions;
using UnityEditor;

namespace EditorScripts.ReactionEditors
{
    [CustomEditor(typeof(SetConditionReaction))]
    public class SetConditionReactionEditor : ReactionEditor
    {
        protected override string GetFoldoutLabel()
        {
            return "Set Condition Reaction";
        }
    }
}
