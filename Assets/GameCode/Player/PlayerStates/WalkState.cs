using LockdownGames.GameAi.StateMachineAi;
using LockdownGames.Mechanics.ActorMechanics.MovementMechanics;

namespace LockdownGames.GameCode.Player
{
    public class MoveState : State<PlayerController>
    {
        private RigidBodyMovement playerMovement;
        
        public MoveState(PlayerController stateMachine)
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
            playerMovement.WalkToNextPoint();

            // if seeker is not done yet or it was not able to move
            if (playerMovement.IsMoving)
            {
                return;
            }

            //Debug.Log("Stopping because current speed is : " + playerMovement.currentSpeed);
            stateMachine.SetStateTo<IdleState>();
        }
    }
}
