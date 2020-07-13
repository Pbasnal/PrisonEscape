using LockdownGames.GameAi.StateMachineAi;
using LockdownGames.Mechanics.ActorMechanics.MovementMechanics;

using UnityEngine;

namespace LockdownGames.GameCode.Player
{
    public class FollowState : State<PlayerAi>
    {
        private FollowMechanics followMechanics;

        public override void SetState(StateMachine sm)
        {
            base.SetState(sm);
            followMechanics = stateMachine.GetComponent<FollowMechanics>();
        }

        public override void End()
        { }

        public override void Start()
        { }

        public override void FixedUpdate()
        {
            followMechanics.Follow();

            // if seeker is not done yet or it was not able to move
            if (followMechanics.IsMoving)
            {
                return;
            }

            //Debug.Log("Stopping because current speed is : " + playerMovement.currentSpeed);
            stateMachine.SetStateTo<IdleState>();
        }

        public override void Update()
        {
            
        }
    }
}
