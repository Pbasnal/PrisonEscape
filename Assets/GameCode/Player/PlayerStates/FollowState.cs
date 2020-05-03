using LockdownGames.GameAi.StateMachineAi;
using LockdownGames.Mechanics.ActorMechanics.MovementMechanics;

using UnityEngine;

namespace LockdownGames.GameCode.Player
{
    public class FollowState : State<PlayerAi>
    {
        private FollowMechanics followMechanics;

        public FollowState(PlayerAi stateMachine)
            : base(stateMachine)
        {
            followMechanics = stateMachine.GetComponent<FollowMechanics>();
        }

        public override void End()
        { }

        public override void Start()
        { }

        public override void Update()
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
    }
}
