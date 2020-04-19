using GameCode.InteractionSystem;
using UnityEngine;

namespace GameCode.Mechanics.InteractionSystem.Reactions.ImmediateReactions
{
    public class PickedUpKeyReaction : Reaction
    {
        public Condition ConditionToSet;

        protected override void ImmediateReaction()
        {
            if (ConditionToSet.IsSatisfied)
            {
                Debug.Log("Condition is already set");
                return;
            }

            foreach (var condition in AllConditions.Instance.Conditions)
            {
                if (condition.Hash != ConditionToSet.Hash)
                {
                    continue;
                }

                Debug.Log("Setting key condition to true");
                condition.IsSatisfied = true;
            }
        }
    }
}
