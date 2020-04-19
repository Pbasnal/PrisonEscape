using GameCode.Mechanics.InteractionSystem.Reactions.ImmediateReactions;
using UnityEditor;

namespace EditorScripts.ReactionEditors
{
    [CustomEditor(typeof(CloseDoorReaction))]
    public class CloseDoorReactionEditor : ReactionEditor
    {
        protected override string GetFoldoutLabel()
        {
            return "Close Door Reaction";
        }
    }

}
