using GameAi.FiniteStateMachine;
using System.Collections;
using UnityEngine;

namespace GameAi.ZombieStates
{
    public class WanderingState : State
    {
        private ZombieStateMachine zombieStateMachine => (ZombieStateMachine)StateMachine;

        private int[][] posUpdatematrix;
        private int currentUpdateIndex = 0;

        public WanderingState(StateMachine stateMachine) : base(stateMachine)
        {
            posUpdatematrix = new int[4][];

            posUpdatematrix[0] = new int[] { -1, 0 };
            posUpdatematrix[1] = new int[] { 1, 0 };
            posUpdatematrix[2] = new int[] { 0, 1 };
            posUpdatematrix[3] = new int[] { 0, -1 };
        }

        public override IEnumerator ProcessState()
        {
            //Debug.Log("Calling Wandering move");

            if (zombieStateMachine.IsPlayerInView())
            {
                zombieStateMachine.SetState(new HuntPlayerState(zombieStateMachine));
                yield break;
            }

            var target = GetTarget();

            if (zombieStateMachine.PathIsBlocked(target))
            {
                target = GetTarget(true);
                yield return zombieStateMachine.transform.position;
            }

            var pos = (Vector2)zombieStateMachine.transform.position;
            var dir = (target - pos).normalized;
            zombieStateMachine.MoveInDirection(dir);
        }

        public Vector2 GetTarget(bool updateIndex = false)
        {
            var pos = (Vector2)StateMachine.transform.position;
            var target = new Vector2(pos.x + posUpdatematrix[currentUpdateIndex][0], pos.y + posUpdatematrix[currentUpdateIndex][1]);

            if (!updateIndex)
            {
                return target;
            }

            currentUpdateIndex = Random.Range(0, 4);

            return target;
        }
    }
}
