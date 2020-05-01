using System.Collections;
using LockdownGames.GameCode.GameAi.Code.FiniteStateMachine;

namespace LockdownGames.GameCode.GameAi.Code.ZombieStates
{
    public class DummyWanderingState : State
    {
        private ZombieStateMachine zombieStateMachine => (ZombieStateMachine)StateMachine;

        public DummyWanderingState(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override IEnumerator ProcessState()
        {
            var playersInView = zombieStateMachine.IsPlayerInView();

            if (playersInView)
            {
                zombieStateMachine.SetState(new HuntPlayerState(zombieStateMachine));
            }
            yield return null;
        }
    }
}
