using GameAi.StateMachine2;
using GameCode.Mechanics.PlayerMechanics;
using UnityEngine;

namespace GameCode.Player.PlayerStates
{
    public class MoveState : State<PlayerStateMachine>
    {
        private RigidBodyMovement playerMovement;
        
        public MoveState(PlayerStateMachine stateMachine)
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
            if (!playerMovement.WalkToNextPoint() || playerMovement.currentSpeed > 0.001f)
            {
                return;
            }

            //Debug.Log("Stopping because current speed is : " + playerMovement.currentSpeed);
            stateMachine.SetStateTo<IdleState>();
        }
    }
}
