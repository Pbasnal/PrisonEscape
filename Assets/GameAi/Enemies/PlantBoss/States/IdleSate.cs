using System.Runtime.InteropServices;
using LockdownGames.GameAi.StateMachineAi;
using LockdownGames.Mechanics.AiMechanics;

namespace LockdownGames.GameAi.Enemies.PlantBoss.States
{
    public class IdleSate : State<PlantBossAi>
    {
        //public IdleSate(PlantBossAi stateMachine) 
        //    : base(stateMachine)
        //{}

        public override void FixedUpdate()
        {
        }

        public override void End()
        {}

        public override void Start()
        {}

        public override void Update()
        {
            var target = stateMachine.FindPlayerInFov();
            if (target == null)
            {
                return;
            }
        }
    }
}
