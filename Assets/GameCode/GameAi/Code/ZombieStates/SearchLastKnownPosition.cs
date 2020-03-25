using GameAi.FiniteStateMachine;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameAi.ZombieStates
{
    public class SearchLastKnownPosition : State
    {
        private ZombieStateMachine zombieStateMachine => (ZombieStateMachine)StateMachine;

        private List<Vector2> zombiePath;
        private int currentPathIndex;

        private bool searchingForPath;

        public SearchLastKnownPosition(StateMachine stateMachine) : base(stateMachine)
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
            zombieStateMachine.GetPathToLastKnownPlayerPosition(OnPathComplete);

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

            if (zombieStateMachine.IsPlayerInView())
            {
                zombieStateMachine.SetState(new HuntPlayerState(zombieStateMachine));
                yield break;
            }

            if (currentPathIndex == zombiePath.Count)
            {
                zombieStateMachine.SetState(new WanderingState(zombieStateMachine));
                yield break;
            }

            var target = zombiePath[currentPathIndex];

            var pos = (Vector2)zombieStateMachine.transform.position;
            var dir = (target - pos).normalized;
            zombieStateMachine.RunInDirection(dir);

            // -1 because - if index = 0 and count = 1, it'll increment the index.
            // while getting the target in next frame, zombiePath[1] will throw exception
            if (currentPathIndex < zombiePath.Count && Vector2.Distance(pos, target) <= 0.05f)
            {
                currentPathIndex++;
            }

            yield break;
        }

        private void OnPathComplete(Path p)
        {
            if (p.error)
            {
                return;
            }

            searchingForPath = false;
            zombiePath.Clear();
            currentPathIndex = 1; // because index 0 is the position of the seeker
            zombiePath = p.vectorPath.Select(v => (Vector2)v).ToList();
        }
    }
}