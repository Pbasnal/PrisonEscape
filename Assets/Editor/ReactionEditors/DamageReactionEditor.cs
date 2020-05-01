using LockdownGames.Mechanics.InteractionSystem.Reactions.ImmediateReactions;

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
