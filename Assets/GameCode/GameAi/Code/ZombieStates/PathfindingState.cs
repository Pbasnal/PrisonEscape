using System.Collections;

using LockdownGames.GameCode.GameAi.Code.FiniteStateMachine;

using Pathfinding;

using UnityEngine;

namespace LockdownGames.GameCode.GameAi.Code.ZombieStates
{
    public class PathfindingState : State
    {
        public static PathfindingState instance;
        private ZombieStateMachine zombieStateMachine => (ZombieStateMachine)StateMachine;

        private int[][] posUpdatematrix;
        private int currentUpdateIndex = 0;

        private Vector2 _target;

        public PathfindingState(ZombieStateMachine stateMachine) : base(stateMachine)
        {
            posUpdatematrix = new int[4][];

            posUpdatematrix[0] = new int[] { -1, 0 };
            posUpdatematrix[1] = new int[] { 1, 0 };
            posUpdatematrix[2] = new int[] { 0, 1 };
            posUpdatematrix[3] = new int[] { 0, -1 };

            instance = this;
        }

        public override IEnumerator Start()
        {
            Debug.Log("Calling pathfinding start");

            _target = GetTarget();
            if (zombieStateMachine.PathIsBlocked(_target))
            {
                _target = GetTarget(updateIndex: true);
                zombieStateMachine.SetState(this);
                yield break;
            }

            zombieStateMachine.GetPathTo(_target, OnPathComplete);
            zombieStateMachine.SetState(new ThinkingState(zombieStateMachine));
            yield return null;
        }

        private void OnPathComplete(Path p)
        {
            Debug.Log("Found new path");
            if (p.error)
            {
                _target = GetTarget();
                zombieStateMachine.SetState(this);
                return;
            }

            zombieStateMachine.path = p;
            zombieStateMachine.SetState(new WanderingState(zombieStateMachine));
        }

        public Vector2 GetTarget(bool updateIndex = false)
        {
            var pos = StateMachine.transform.position;
            pos = new Vector2(pos.x + posUpdatematrix[currentUpdateIndex][0], pos.y + posUpdatematrix[currentUpdateIndex][1]);

            if (updateIndex)
            {
                currentUpdateIndex = Random.Range(0, 4);
                Debug.Log("Random index: " + currentUpdateIndex);
            }

            return pos;
        }
    }
}
