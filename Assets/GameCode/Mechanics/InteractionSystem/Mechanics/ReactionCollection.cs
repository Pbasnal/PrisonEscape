﻿using System;
using UnityEngine;

namespace GameCode.InteractionSystem
{
    public class ReactionCollection : MonoBehaviour
    {
        public Reaction[] Reactions = new Reaction[0];

        private void Start()
        {
            for (int i = 0; i < Reactions.Length; i++)
            {
                var delayedReaction = Reactions[i] as DelayedReaction;

                if (delayedReaction)
                    delayedReaction.Init();
                else
                    Reactions[i].Init();
            }
        }

        public void React()
        {
            for (int i = 0; i < Reactions.Length; i++)
            {
                var delayedReaction = Reactions[i] as DelayedReaction;

                if (delayedReaction)
                    delayedReaction.React(this);
                else
                    Reactions[i].React(this);
            }
        }
    }
}