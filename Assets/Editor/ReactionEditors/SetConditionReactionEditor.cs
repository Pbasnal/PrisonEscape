using LockdownGames.GameCode.Mechanics.InteractionSystem.Mechanics.Reactions.ImmediateReactions;

using UnityEditor;

namespace LockdownGames.EditorScripts.ReactionEditors
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
