using LockdownGames.GameAi.StateMachineAi;

namespace LockdownGames.GameAi.Enemies.PlantBoss.States
{
    public class AttackState : State<PlantBossAi>
    {
        public AttackState(PlantBossAi stateMachine) : base(stateMachine)
        {
        }

        public override void End()
        {
        }

        public override void Start()
        {
        }

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
