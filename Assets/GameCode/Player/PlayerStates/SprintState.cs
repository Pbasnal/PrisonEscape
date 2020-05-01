using GameAi.StateMachine2;
using LockdownGames.GameCode.Mechanics.ActorMechanics;

namespace LockdownGames.GameCode.Player
{
    public class SprintState : State<PlayerStateMachine>
    {
        private RigidBodyMovement playerMovement;

        public SprintState(PlayerStateMachine stateMachine)
            : base(stateMachine)
        {
            playerMovement = stateMachine.GetComponent<RigidBodyMovement>();
        }

        public override void End()
        { }

        public override void Start()
        { }

        public override void Update()
        {
            // if seeker is not done yet or it was not able to move
            if (!playerMovement.RunToNextPoint() || playerMovement.currentSpeed > 0.001f)
            {
                return;
            }

            stateMachine.SetStateTo<IdleState>();
        }
    }
}