using LockdownGames.GameAi.StateMachineAi;

namespace LockdownGames.GameCode.Player
{
    public class InteractionState : State<PlayerController>
    {
        public InteractionState(PlayerController stateMachine) : base(stateMachine)
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