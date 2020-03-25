using GameAi.FiniteStateMachine;
using System.Collections;

namespace GameAi.ZombieStates
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
