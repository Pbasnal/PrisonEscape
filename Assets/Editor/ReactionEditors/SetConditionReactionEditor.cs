using LockdownGames.Mechanics.InteractionSystem.Reactions.ImmediateReactions;

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
