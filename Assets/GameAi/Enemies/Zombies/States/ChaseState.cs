using LockdownGames.GameCode.GameAi.StateMachine2;
using LockdownGames.Mechanics.ActorMechanics.MovementMechanics;

namespace LockdownGames.GameAi.Enemies.Zombies
{
    public class ChaseState : State<ZombieStateMachine2>
    {
        private RigidBodyMovement mover;

        public ChaseState(ZombieStateMachine2 stateMachine)
            : base(stateMachine)
        {
            mover = stateMachine.GetComponent<RigidBodyMovement>();
        }

        public override void End()
        {}

        public override void Start()
        {}

        public override void Update()
        {
            if (stateMachine.target == null)
            {
                stateMachine.SetStateTo<SearchLastKnownPositionState>();

                return;
            }

            mover.SetPathTo(stateMachine.target.position);
            mover.RunToNextPoint();
        }
    }
}

