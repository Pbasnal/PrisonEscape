using LockdownGames.Mechanics.InteractionSystem.Reactions.ImmediateReactions;

using UnityEditor;

namespace LockdownGames.EditorScripts.ReactionEditors
{
    [CustomEditor(typeof(PlayAudioReaction))]
    public class PlayAudioReactionEditor : ReactionEditor
    {
        protected override string GetFoldoutLabel()
        {
            return "Play Audio Reaction";
        }
    }

}
