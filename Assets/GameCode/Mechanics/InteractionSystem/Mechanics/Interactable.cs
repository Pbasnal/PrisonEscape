using UnityEngine;

namespace GameCode.InteractionSystem.Mechanics
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
