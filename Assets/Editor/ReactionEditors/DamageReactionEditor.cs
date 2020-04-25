using GameCode.InteractionSystem.Reactions.ImmediateReactions;
using UnityEditor;

namespace EditorScripts.ReactionEditors
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
