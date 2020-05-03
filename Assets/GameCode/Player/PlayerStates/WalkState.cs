using LockdownGames.GameAi.StateMachineAi;
using LockdownGames.Mechanics.ActorMechanics.MovementMechanics;
using LockdownGames.Mechanics.InputMechanics;

namespace LockdownGames.GameCode.Player
{
    public class WalkState : State<PlayerAi>
    {
        private GestureInput gestureInput;
        private RigidBodyMovement playerMovement;
        
        public WalkState(PlayerAi stateMachine)
            : base(stateMachine)
        {
            gestureInput = stateMachine.GetComponent<GestureInput>();
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
