using GameCode.GameAi.Code;
using System.Collections;
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
        public Animator Animator;

        private Vector2 currentPosition => (Vector2)transform.position;
        private Rigidbody2D rigidBody;

        private WeaponHolder weaponHolder;
        private MotionState MotionState;

        private void Start()
        {
            weaponHolder = GetComponent<WeaponHolder>();
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            int h = (int)Input.GetAxisRaw("Horizontal");
            int v = (int)Input.GetAxisRaw("Vertical");

            rigidBody.velocity = new Vector3(h * MoveSpeed, v * MoveSpeed);

            if (Input.GetKeyDown(KeyCode.Space) && weaponHolder.HasWeapon)
            {
                var dir = Attack();
                MotionState = GetMotionState((int)dir.x, (int)dir.y);

                //Set animator properties
                Animator.SetTrigger("Attack");
            }
            else
            {
                MotionState = GetMotionState(h, v);
            }

            Debug.Log(MotionState.ToString());
            Animator.SetInteger("MotionState", (int)MotionState);
        }

        private IEnumerator SetAttackingToFalse()
        {
            yield return new WaitForSecondsRealtime(0.1f);
            Animator.SetBool("IsAttacking", false);
            yield break;
        }

        private MotionState GetMotionState(int h, int v)
        {
            //Debug.Log("h and v " + h + "  &  " + v);
            if (h == 0 && v == 0)
            {
                return MotionState.Idle;
            }

            if (h != 0)
            {
                return h < 0 ? MotionState.WalkingLeft : MotionState.WalkingRight;
            }

            return v < 0 ? MotionState.WalkingDown : MotionState.WalkingUp;
        }

        private Vector2 Attack()
        {
            var enemy = GetClosestEnemy();

            var direction = GetEnemyDirection(enemy);
            weaponHolder.Attack(direction);

            return direction;
        }

        private Transform GetClosestEnemy()
        {
            var enemyColliders = Physics2D.OverlapCircleAll(currentPosition, AttackRange, EnemyLayerMask);

            if (enemyColliders == null || enemyColliders.Length == 0)
            {
                return null;
            }

            Transform closestEnemy = null;
            float distanceFromClosestEnemy = float.MaxValue;

            foreach (var enemyCollider in enemyColliders)
            {
                if (Vector2.Distance(enemyCollider.transform.position, transform.position) >= distanceFromClosestEnemy)
                {
                    continue;
                }

                closestEnemy = enemyCollider.transform;
                distanceFromClosestEnemy = Vector2.Distance(enemyCollider.transform.position, transform.position);
            }

            return closestEnemy;
        }

        private Vector2 GetEnemyDirection(Transform closestEnemy)
        {
            if (closestEnemy == null)
            {
                return new Vector2((int)Input.GetAxisRaw("Horizontal"), (int)Input.GetAxisRaw("Vertical"));
            }

            var dir = (Vector2)(closestEnemy.position - transform.position);

            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                return (dir.x > 0) ? Vector2.right : Vector2.left;
            }
            else
            {
                return (dir.y > 0) ? Vector2.up : Vector2.down;
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
