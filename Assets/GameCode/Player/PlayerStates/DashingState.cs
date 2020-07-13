using LockdownGames.GameAi.StateMachineAi;
using UnityEngine;

namespace LockdownGames.GameCode.Player
{
    public class DashingState : State<PlayerAi>
    {
        public Vector3 target;

        //public DashingState(PlayerAi stateMachine)
        //    : base(stateMachine)
        //{}

        public override void End()
        { }

        public override void Start()
        {
            target = stateMachine.target;
            Debug.DrawLine(stateMachine.currentPosition, stateMachine.target, Color.blue, 5);
        }

        public override void FixedUpdate()
        {
            stateMachine.movementController.Move(target, stateMachine.dashSpeed);
            var distance = Vector3.Distance(target, stateMachine.currentPosition);
            if (distance > 0.5f)
            {
                return;
            }

            stateMachine.SetStateTo<IdleState>();
        }

        public override void Update()
        {
            
        }
    }
}
