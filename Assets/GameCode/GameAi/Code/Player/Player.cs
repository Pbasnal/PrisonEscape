using UnityEngine;

namespace GameAi.Code.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        public float MoveSpeed;
        public int AttackDamage;
        public float AttackRange;
        public LayerMask EnemyLayerMask;

        private Vector2 currentPosition => (Vector2)transform.position;
        private Rigidbody2D rigidBody;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            var h = Input.GetAxisRaw("Horizontal");
            var v = Input.GetAxis("Vertical");

            rigidBody.velocity = new Vector3(h * MoveSpeed, v * MoveSpeed);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
        }

        private void Attack()
        {
            var enemyColliders = Physics2D.OverlapCircleAll(currentPosition, 1.5f, EnemyLayerMask);

            foreach (var enemyCollider in enemyColliders)
            {
                enemyCollider.GetComponent<ZombieAi>().TakeDamage(transform, AttackDamage);
            }
        }

        // returns int
        // in scenarios where damaging player deals some damage to the attacker itself
        public int TakeDamage(int damage)
        {
            Debug.Log("Took damage " + damage);
            return 0;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(currentPosition, AttackRange);
        }
    }
}
