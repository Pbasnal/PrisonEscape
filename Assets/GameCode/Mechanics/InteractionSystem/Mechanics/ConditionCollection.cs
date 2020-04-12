﻿using UnityEngine;

namespace GameCode.InteractionSystem
{
    public class ConditionCollection : ScriptableObject
    {
        public string Description;
        public Condition[] RequiredConditions = new Condition[0];
        public ReactionCollection ReactionCollection;

        public bool CheckAndReact()
        {
            for (int i = 0; i < RequiredConditions.Length; i++)
            {
                if (!AllConditions.CheckCondition(RequiredConditions[i]))
                {
                    return false;
                }
            }

            ReactionCollection?.React();

            return true;
        }
    }
}
