using GameAi.FiniteStateMachine;
using System.Collections;
using UnityEngine;

namespace GameAi.ZombieStates
{
    public class AttackState : State
    {
        private float timeBeforeNextAttack = 0;
        private ZombieStateMachine zombieStateMachine => (ZombieStateMachine)StateMachine;

        public AttackState(StateMachine stateMachine) : base(stateMachine)
        {
            zombieStateMachine.animator.SetBool("IsAttacking", true);
        }

        public override IEnumerator ProcessState()
        {
            Debug.Log("Start attacking");

            if (timeBeforeNextAttack > 0.1)
            {
                timeBeforeNextAttack -= Time.deltaTime;
                yield break;
            }

            if (zombieStateMachine.AttackPlayer() != ZombieStateMachine.HaveCaughtPlayer)
            {
                zombieStateMachine.animator.SetBool("IsAttacking", false);
                zombieStateMachine.SetState(new SearchLastKnownPosition(zombieStateMachine));
            }

            timeBeforeNextAttack = 1;
            yield break;
        }
    }
}
