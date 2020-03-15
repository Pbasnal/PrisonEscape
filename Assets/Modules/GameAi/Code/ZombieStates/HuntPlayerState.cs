using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameAi.FiniteStateMachine;
using Pathfinding;
using UnityEngine;

namespace GameAi.ZombieStates
{
    public class HuntPlayerState : State
    {
        private ZombieStateMachine zombieStateMachine => (ZombieStateMachine)StateMachine;

        private List<Vector2> zombiePath;
        private int currentPathIndex;

        private bool searchingForPath;

        public HuntPlayerState(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override IEnumerator Start()
        {
            if (zombiePath == null)
            {
                zombiePath = new List<Vector2>();
            }

            zombiePath.Clear();
            zombiePath.Add(zombieStateMachine.transform.position);
            currentPathIndex = 0;

            searchingForPath = true;
            zombieStateMachine.GetPathToPlayer(OnPathComplete);

            yield break;
        }

        // this will be called per frame
        public override IEnumerator ProcessState()
        {
            Debug.Log("Calling chase");

            if (searchingForPath)
            {
                yield break;
            }

            var target = zombiePath[currentPathIndex];

            var pos = (Vector2)zombieStateMachine.transform.position;
            var dir = (target - pos).normalized;
            zombieStateMachine.RunInDirection(dir);

            // -1 because - if index = 0 and count = 1, it'll increment the index.
            // while getting the target in next frame, zombiePath[1] will throw exception
            if (currentPathIndex < zombiePath.Count - 1 && Vector2.Distance(pos, target) <= 0.01f)
            {
                currentPathIndex++;
            }

            if (!zombieStateMachine.IsPlayerInView())
            {
                zombieStateMachine.SetState(new SearchLastKnownPosition(zombieStateMachine));
            }

            searchingForPath = true;
            if (zombieStateMachine.GetPathToPlayer(OnPathComplete) == ZombieStateMachine.HaveCaughtPlayer)
            {
                zombieStateMachine.SetState(new AttackState(zombieStateMachine));
            }

            yield break;
        }

        private void OnPathComplete(Path p)
        {
            if (p.error)
            {
                return;
            }
            Debug.Log("Found new path");
            searchingForPath = false;
            zombiePath.Clear();
            currentPathIndex = 1; // because index 0 is the position of the seeker
            zombiePath = p.vectorPath.Select(v => (Vector2)v).ToList();
        }
    }
}