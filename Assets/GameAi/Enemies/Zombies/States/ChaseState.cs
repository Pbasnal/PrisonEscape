using LockdownGames.GameAi.StateMachineAi;
using LockdownGames.Mechanics.ActorMechanics.MovementMechanics;

namespace LockdownGames.GameAi.Enemies.Zombies
{
    public class ChaseState : State<ZombieAi>
    {
        private RigidBodyMovement mover;

        public override void SetState(StateMachine sm)
        {
            base.SetState(sm);
            mover = stateMachine.GetComponent<RigidBodyMovement>();
        }

        public override void FixedUpdate()
        {
            if (stateMachine.target == null)
            {
                stateMachine.SetStateTo<SearchLastKnownPositionState>();

                return;
            }

            mover.SetPathTo(stateMachine.target.position);
            mover.RunToNextPoint();
        }

        public override void End()
        {}

        public override void Start()
        {}

        public override void Update()
        {
            
        }
    }
}

