using LockdownGames.GameCode.GameAi.StateMachine2;

namespace LockdownGames.GameCode.Player
{
    public class InteractionState : State<PlayerStateMachine>
    {
        public InteractionState(PlayerStateMachine stateMachine) : base(stateMachine)
        {}

        public override void End()
        {}

        public override void Start()
        {}

        public override void Update()
        {
            stateMachine.SetStateTo<IdleState>();
        }
    }
}