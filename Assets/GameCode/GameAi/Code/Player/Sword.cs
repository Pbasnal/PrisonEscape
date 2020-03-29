using UnityEngine;

namespace GameAi.Code.Player
{
    public class Sword : Weapon
    {
        public int AttackDamage;
        public float AttackRange;
        public Transform HitPoint;
        public LayerMask EnemyLayerMask;

        public override void Attack()
        {
            var pos = transform.position;
            var size = GetSize();

            var enemyColliders = Physics2D.OverlapCircleAll(HitPoint.position, AttackRange, EnemyLayerMask);

            if (enemyColliders == null || enemyColliders.Length == 0)
            {
                return;
            }

            foreach (var enemy in enemyColliders)
            {
                var zombie = enemy.GetComponent<ZombieAi>();

                if (zombie == null)
                {
                    continue;
                }

                zombie.TakeDamage(transform, AttackDamage);
            }
        }

        private Vector2 GetSize()
        {
            Vector2 size;
            if ((int)transform.rotation.z == 90)
            {
                // after rotating 90, -y axis becomes +x
                //pos = new Vector2(transform.position.x, transform.position.y - AttackRange);
                size = new Vector2(0.5f, AttackRange);
            }
            else if ((int)transform.rotation.z == -90)
            {
                //pos = new Vector2(transform.position.x - AttackRange / 2, transform.position.y);
                size = new Vector2(-AttackRange, 0.5f);
            }
            else if ((int)transform.rotation.z == 180)
            {
                //pos = new Vector2(transform.position.x, transform.position.y + AttackRange / 2);
                size = new Vector2(0.5f, AttackRange);
            }
            else
            {
                //pos = new Vector2(transform.position.x, transform.position.y - AttackRange / 2);
                size = new Vector2(0.5f, -AttackRange);
            }

            return size;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(HitPoint.position, AttackRange);
        }
    }
}
