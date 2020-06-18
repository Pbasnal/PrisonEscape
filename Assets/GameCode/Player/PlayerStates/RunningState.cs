using LockdownGames.GameAi.StateMachineAi;

namespace LockdownGames.GameCode.Player
{
    public class RunningState : State<PlayerAi>
    {
        public RunningState(PlayerAi stateMachine)
            : base(stateMachine)
        {}

        public override void End()
        { }

        public override void Start()
        {
            stateMachine.mover.SetPathTo(stateMachine.target);
        }

        public override void Update()
        {
            stateMachine.mover.MoveToNextWayPoint(stateMachine.runningSpeed);
            if (stateMachine.mover.IsMoving)
            {
                return;
            }

            stateMachine.SetStateTo<IdleState>();
        }
    }
}