using LockdownGames.GameCode.Mechanics.InteractionSystem.Mechanics.Reactions.ImmediateReactions;

using UnityEditor;

namespace LockdownGames.EditorScripts.ReactionEditors
{
    [CustomEditor(typeof(OpenDoorReaction))]
    public class OpenDoorReactionEditor : ReactionEditor
    {
        protected override string GetFoldoutLabel()
        {
            return "Open Door Reaction";
        }
    }

}
