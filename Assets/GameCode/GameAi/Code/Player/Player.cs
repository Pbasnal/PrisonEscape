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

        private MotionState MotionState;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            int h = (int)Input.GetAxisRaw("Horizontal");
            int v = (int)Input.GetAxisRaw("Vertical");

            rigidBody.velocity = new Vector3(h * MoveSpeed, v * MoveSpeed);

            MotionState = GetMotionState(h, v);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //MotionState = GetAttackState();
                Animator.SetTrigger("Attack");
                Debug.Log(MotionState.ToString());
                Attack();
                //StartCoroutine("SetAttackingToFalse");
            }
            else
            {
                Animator.SetInteger("MotionState", (int)MotionState);
            }
            //Debug.Log("Current motionState: " + MotionState);
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

            if(h != 0) return h < 0 ? MotionState.WalkingLeft: MotionState.WalkingRight;
            return v < 0 ? MotionState.WalkingDown : MotionState.WalkingUp;
        }

        private MotionState GetAttackState()
        {
            if (MotionState == MotionState.Idle) return MotionState.AttackDown;
            return MotionState + 4;
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
