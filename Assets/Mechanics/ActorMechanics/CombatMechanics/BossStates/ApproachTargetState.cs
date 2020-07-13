using System;
using LockdownGames.GameAi.StateMachineAi;
using LockdownGames.Mechanics.ActorMechanics.CombatMechanics.TargetApproach;
using LockdownGames.Utilities;
using UnityEngine;

namespace LockdownGames.Mechanics.ActorMechanics.CombatMechanics.BossStates
{
    [Serializable]
    public class ApproachTargetState : State<Enemy>
    { 
        public DebugLogger logger;
        private ApproachTarget approachTarget;

        public override void SetState(StateMachine sm)
        {
            base.SetState(sm);
            approachTarget = stateMachine.approachTarget;
        }

        public override void End()
        {
        }

        public override void FixedUpdate()
        {
            var distance = Vector3.Distance(stateMachine.transform.position, stateMachine.target.position);

            //if (approachTarget.npcApproachData.detectionDistance + 3 < distance)
            //{
            //    approachTarget.EndApproach();
            //    stateMachine.SetStateTo<IdleState>();
            //}
        }

        public override void Start()
        {
            logger.Log("Setting target to: {0}", stateMachine.target.position);
            approachTarget.Approach(stateMachine.target, stateMachine.enemyStats.baseSpeed);
        }

        public override void Update()
        { }
    }
}
