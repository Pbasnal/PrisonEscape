﻿using GameCode.InteractionSystem.Mechanics;
using UnityEngine;

namespace GameCode.InteractionSystem
{
    public class ReactionCollection : MonoBehaviour
    {
        public Reaction[] Reactions = new Reaction[0];
        
        private Interactable interactable; 

        private void Start()
        {
            interactable = GetComponentInParent<Interactable>();

            for (int i = 0; i < Reactions.Length; i++)
            {
                var delayedReaction = Reactions[i] as DelayedReaction;

                if (delayedReaction)
                    delayedReaction.Init();
                else
                    Reactions[i].Init();
            }
        }

        public void React(MonoBehaviour behaviour)
        {
            for (int i = 0; i < Reactions.Length; i++)
            {
                var delayedReaction = Reactions[i] as DelayedReaction;

                if (delayedReaction)
                    delayedReaction.React(behaviour);
                else
                    Reactions[i].React(behaviour, interactable);
            }
        }
    }
}