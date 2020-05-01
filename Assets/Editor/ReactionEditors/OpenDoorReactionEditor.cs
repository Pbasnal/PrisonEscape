using LockdownGames.Mechanics.InteractionSystem.Reactions.ImmediateReactions;

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
