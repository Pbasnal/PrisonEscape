using LockdownGames.Mechanics.InteractionSystem.Conditions;
using LockdownGames.Mechanics.InteractionSystem.Reactions;
using UnityEngine;

namespace LockdownGames.Mechanics.InteractionSystem
{
    public class Interactable : MonoBehaviour
    {
        public Transform interactionLocation;
        public ConditionCollection[] conditionCollections = new ConditionCollection[0];
        public ReactionCollection defaultReactionCollection;

        public void Interact(MonoBehaviour behaviour)
        {
            for (int i = 0; i < conditionCollections.Length; i++)
            {
                if (conditionCollections[i].CheckAndReact(this))
                {
                    return;
                }
            }

            defaultReactionCollection?.React(behaviour);
        }
    }
}
