using LockdownGames.Mechanics.ActorMechanics.CombatMechanics;
using UnityEngine;

namespace LockdownGames.Assets.Mechanics.ActorMechanics.CombatMechanics
{
    public class AttackOnContactMechanics : MonoBehaviour, ICanAttack
    {
        public float damageToDeal;

        public float attackInterval;


        private float timeSinceLastAttack;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var canTakeDamage = collision.collider.GetComponent<ICanTakeDamage>();
            if (canTakeDamage == null)
            {
                return;
            }

            timeSinceLastAttack = 0;
            Attack(canTakeDamage);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            var canTakeDamage = collision.collider.GetComponent<ICanTakeDamage>();
            if (canTakeDamage == null)
            {
                return;
            }

            if (timeSinceLastAttack < attackInterval)
            {
                timeSinceLastAttack += Time.deltaTime;
                return;
            }

            timeSinceLastAttack = 0;
            Attack(canTakeDamage);
        }

        public void Attack(ICanTakeDamage canTakeDamage)
        {
            canTakeDamage.TakeDamage(damageToDeal);
        }
    }
}
