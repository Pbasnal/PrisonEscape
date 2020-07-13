using System;
using LockdownGames.GameAi.StateMachineAi;
using LockdownGames.Mechanics.ActorMechanics.MovementMechanics;

namespace LockdownGames.Mechanics.ActorMechanics.CombatMechanics.BossStates
{
    [Serializable]
    public class WanderingState : State<Enemy>
    {
        private SeekerWanderingMechanics wanderingMechanics;

        public override void SetState(StateMachine sm)
        {
            if (stateMachine == null)
            {
                base.SetState(sm);
                wanderingMechanics = stateMachine.GetComponent<SeekerWanderingMechanics>();
            }

            wanderingMechanics.npcStats = stateMachine.enemyStats;
            wanderingMechanics.wanderingTarget = stateMachine.virtualTarget;
        }

        public override void End()
        {}

        public override void Start()
        {}

        public override void FixedUpdate()
        {
            wanderingMechanics.Wander();
        }

        public override void Update()
        {
            if (stateMachine.target == null)
            {
                return;
            }


        }
    }
}

