using LockdownGames.Mechanics.ActorMechanics.CombatMechanics.TakeDamageMechanic;

using UnityEngine;

namespace LockdownGames.Mechanics.CombatMechanics
{
    public enum AttackStage
    {
        Anticipation = 0,
        Contact,
        Recovery,
        Complete
    }

    public class WeaponAttack : MonoBehaviour
    {
        // decide on the input later
        // public Command command;

        public float anticipationTimeInMs;
        public float parryableTimeInMs;
        public float recoveryTimeInMs;

        public float damageMultiplier;

        public int animationState;

        private bool attacked;
        private float attackTimer;

        public AttackStage Attack(ICanTakeDamage canTakeDamage, float baseDamageAmount)
        {
            attackTimer += Time.deltaTime;
            if (!attacked && attackTimer < anticipationTimeInMs)
            {
                Debug.Log(string.Format("Anticipating: {0}", 0));
                return AttackStage.Anticipation;
            }
            else if (!attacked && attackTimer > anticipationTimeInMs)
            {
                canTakeDamage.TakeDamage(baseDamageAmount * damageMultiplier);
                attacked = true;
                return AttackStage.Contact;
            }
            else if (attacked && attackTimer < recoveryTimeInMs + anticipationTimeInMs)
            {
                Debug.Log(string.Format("Recovering: {0}", 0));
                return AttackStage.Recovery;
            }

            return AttackStage.Complete;
        }

        public void ResetTime()
        {
            attackTimer = 0;
            attacked = false;
        }
    }
}
