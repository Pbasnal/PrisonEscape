using UnityEngine;

namespace LockdownGames.Mechanics.InteractionSystem.Reactions.ImmediateReactions
{
    public class PlayAudioReaction : Reaction
    {
        public AudioSource audioSource;
        public AudioClip audioClip;


        protected override void ImmediateReaction()
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}
