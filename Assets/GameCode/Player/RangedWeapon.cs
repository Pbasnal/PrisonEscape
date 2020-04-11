using Assets.GameCode.Interfaces;
using UnityEngine;

namespace GameCode.Player
{
    public abstract class BlastWeapon : Weapon
    {
        public float BlastRadius;

        private Vector2 DetectEnemiesInVicinity()
        {
            var enemyColliders = Physics2D.OverlapCircleAll(transform.position, BlastRadius);

            if (enemyColliders == null || enemyColliders.Length == 0)
            {
                return Vector2.zero;
            }

            Transform closestEnemy = null;
            float distanceFromClosestEnemy = float.MaxValue;

            foreach (var enemyCollider in enemyColliders)
            {
                if (enemyCollider.GetComponent<ICanTakeDamage>() == null)
                {
                    continue;
                }

                var distance = Vector2.Distance(enemyCollider.transform.position, transform.position);
                if (distance >= distanceFromClosestEnemy)
                {
                    continue;
                }

                closestEnemy = enemyCollider.transform;
                distanceFromClosestEnemy = distance;
            }

            return closestEnemy.position - transform.position;
        }
    }
}
