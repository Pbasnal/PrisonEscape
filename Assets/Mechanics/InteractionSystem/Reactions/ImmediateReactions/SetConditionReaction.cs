using LockdownGames.Mechanics.InteractionSystem.Conditions;

namespace LockdownGames.Mechanics.InteractionSystem.Reactions.ImmediateReactions
{
    public class SetConditionReaction : Reaction
    {
        public Condition ConditionToSet;
        public bool setSatisfied;

        protected override void ImmediateReaction()
        {
            foreach (var condition in AllConditions.Instance.Conditions)
            {
                if (condition.Hash != ConditionToSet.Hash)
                {
                    continue;
                }

                condition.IsSatisfied = setSatisfied;
            }
        }
    }
}
