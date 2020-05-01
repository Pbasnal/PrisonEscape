using LockdownGames.GameCode.Mechanics.InteractionSystem.Mechanics.Reactions.ImmediateReactions;

using UnityEditor;

namespace LockdownGames.EditorScripts.ReactionEditors
{
    [CustomEditor(typeof(DamageReaction))]
    public class DamageReactionEditor : ReactionEditor
    {
        protected override string GetFoldoutLabel()
        {
            return "Damage Reaction";
        }
    }

}
