using LockdownGames.GameCode.GameAi.StateMachine2;
using LockdownGames.GameCode.Mechanics.AiMechanics;

namespace LockdownGames.GameCode.Enemies.Zombies.States
{
    public class WanderingState : State<ZombieStateMachine>
    {
        private int[][] posUpdatematrix;
        private int currentUpdateIndex = 0;

        private FieldOfView fov;

        public WanderingState(ZombieStateMachine stateMachine) 
            : base(stateMachine)
        {
            posUpdatematrix = new int[4][];

            posUpdatematrix[0] = new int[] { -1, 0 };
            posUpdatematrix[1] = new int[] { 1, 0 };
            posUpdatematrix[2] = new int[] { 0, 1 };
            posUpdatematrix[3] = new int[] { 0, -1 };
        }

        public override void End()
        {
        }

        public override void Start()
        {
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
    }
}
