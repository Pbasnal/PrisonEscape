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
            zombieStateMachine.animator.SetBool("IsAttacking", true);
        }

        public override IEnumerator ProcessState()
        {
            Debug.Log("Start attacking");

            if (zombieStateMachine.AttackPlayer() != ZombieStateMachine.HaveCaughtPlayer)
            {
                zombieStateMachine.animator.SetBool("IsAttacking", false);
                zombieStateMachine.SetState(new SearchLastKnownPosition(zombieStateMachine));
            }

            yield return new WaitForSecondsRealtime(1);
        }
    }
}
