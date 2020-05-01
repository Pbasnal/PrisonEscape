using LockdownGames.GameAi.StateMachineAi;
using LockdownGames.Mechanics.ActorMechanics.MovementMechanics;

namespace LockdownGames.GameCode.Player
{
    public class SprintState : State<PlayerController>
    {
        private RigidBodyMovement playerMovement;

        public SprintState(PlayerController stateMachine)
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
            playerMovement.RunToNextPoint();

            // if seeker is not done yet or it was not able to move
            if (playerMovement.IsMoving)
            {
                return;
            }

            stateMachine.SetStateTo<IdleState>();
        }
    }
}