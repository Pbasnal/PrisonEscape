using LockdownGames.GameCode.GameAi.StateMachine2;
using LockdownGames.Mechanics.AiMechanics;
using UnityEngine;

namespace LockdownGames.GameAi.Enemies.Zombies
{
    public class WanderingState : State<ZombieStateMachine>
    {
        private Vector2[] posUpdatematrix;
        private int currentUpdateIndex = 0;
        private Vector2 moveDirection;


        private FieldOfView fov;

        public WanderingState(ZombieStateMachine stateMachine) 
            : base(stateMachine)
        {
            posUpdatematrix = new Vector2[]
                {
                    Vector2.left,
                    Vector2.right,
                    Vector2.up,
                    Vector2.down
                };
        }

        public override void End()
        {
        }

        public override void Start()
        {
            // to be updated by checking path block
            moveDirection = GetRandomDirection();

            var collider = stateMachine.GetComponent<Collider2D>();
            
        }

        public override void Update()
        {
            //if (IsPlayerInView())
            //{
            //    zombieStateMachine.SetState(new HuntPlayerState(zombieStateMachine));
            //    yield break;
            //}

            //var target = GetTarget();

            //if (zombieStateMachine.PathIsBlocked(target))
            //{
            //    target = GetTarget(true);
            //    yield return zombieStateMachine.transform.position;
            //}

            //var pos = (Vector2)zombieStateMachine.transform.position;
            //var dir = (target - pos).normalized;
            //zombieStateMachine.MoveInDirection(dir);
        }

        public Vector2 GetRandomDirection()
        {
            currentUpdateIndex = Random.Range(0, 4);

            return posUpdatematrix[currentUpdateIndex];
        }
    }
}
