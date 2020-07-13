using LockdownGames.GameAi.StateMachineAi;

namespace LockdownGames.GameCode.Player
{
    public class WalkState : State<PlayerAi>
    {
        //public WalkState(PlayerAi stateMachine)
        //    : base(stateMachine)
        //{}

        public override void End()
        { }

        public override void Start()
        {
            stateMachine.mover.SetPathTo(stateMachine.target);
        }

        public override void FixedUpdate()
        {
            stateMachine.mover.MoveToNextWayPoint(stateMachine.walkingSpeed);
            if (stateMachine.mover.IsMoving)
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
