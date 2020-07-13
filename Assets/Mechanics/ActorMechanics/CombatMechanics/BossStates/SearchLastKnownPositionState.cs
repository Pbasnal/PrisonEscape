using System;
using LockdownGames.GameAi.StateMachineAi;
using Pathfinding;
using UnityEngine;

namespace LockdownGames.Mechanics.ActorMechanics.CombatMechanics.BossStates
{
    [Serializable]
    public class SearchLastKnownPositionState : State<Enemy>
    {
        public NpcStats npcStats;
        public Transform wanderingTarget;

        private AIPath aiPath;
        private AIDestinationSetter aiDestinationSetter;
        private Vector3 targetPosition;

        public override void SetState(StateMachine sm)
        {
            if (stateMachine == null)
            {
                base.SetState(sm);
                aiPath = sm.GetComponent<AIPath>();
                aiDestinationSetter = sm.GetComponent<AIDestinationSetter>();
            }
            npcStats = stateMachine.enemyStats;
            wanderingTarget = stateMachine.virtualTarget;
        }

        public override void End()
        {
            aiDestinationSetter.target = null;
        }

        public override void Start()
        {
            targetPosition = new Vector3(stateMachine.target.position.x,
                stateMachine.target.position.y,
                stateMachine.target.position.z);

            aiDestinationSetter.target = wanderingTarget;
            aiPath.maxSpeed = npcStats.baseSpeed;
        }

        public override void FixedUpdate()
        { }

        public override void Update()
        {
            wanderingTarget.position = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
            var distance = Vector3.Distance(stateMachine.transform.position, targetPosition);

            if (distance <= 1)
            {
                stateMachine.SetStateTo<WanderingState>();
            }
        }
    }
}

