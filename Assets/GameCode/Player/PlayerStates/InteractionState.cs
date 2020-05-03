using LockdownGames.GameAi.StateMachineAi;

namespace LockdownGames.GameCode.Player
{
    public class InteractionState : State<PlayerAi>
    {
        public InteractionState(PlayerAi stateMachine) : base(stateMachine)
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