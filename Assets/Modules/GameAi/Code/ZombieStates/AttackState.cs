using System.Collections;
using GameAi.FiniteStateMachine;
using UnityEngine;

namespace GameAi.ZombieStates
{
    public class AttackState : State
    {
        private ZombieStateMachine zombieStateMachine => (ZombieStateMachine)StateMachine;

        public AttackState(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override IEnumerator ProcessState()
        {
            Debug.Log("Start attacking");

            if (zombieStateMachine.AttackPlayer() != ZombieStateMachine.HaveCaughtPlayer)
            {
                zombieStateMachine.SetState(new SearchLastKnownPosition(zombieStateMachine));
            }

            yield return new WaitForSecondsRealtime(1);
        }
    }
}
