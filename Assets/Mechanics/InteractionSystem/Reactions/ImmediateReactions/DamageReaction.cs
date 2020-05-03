using LockdownGames.Mechanics.ActorMechanics.CombatMechanics.TakeDamageMechanic;

using UnityEngine;

namespace LockdownGames.Mechanics.InteractionSystem.Reactions.ImmediateReactions
{
    public class DamageReaction : Reaction
    {
        public int damageAmount;
        public float damageInterval;

        private float timeSinceLastDamage = 0.0f;

        protected override void SpecificInit()
        {
            timeSinceLastDamage = damageInterval + 1;
        }

        public override void React(MonoBehaviour monoBehaviour, Interactable interactable)
        {
            var damageTaker = monoBehaviour.GetComponent<ICanTakeDamage>();

            if (damageTaker == null)
            {
                return;
            }

            if (timeSinceLastDamage < damageInterval)
            {
                timeSinceLastDamage += Time.deltaTime;
                return;
            }
            timeSinceLastDamage = 0.0f;

            damageTaker.TakeDamage(damageAmount);
        }

        protected override void ImmediateReaction()
        {}
    }
}
