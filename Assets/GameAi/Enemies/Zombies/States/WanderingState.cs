using LockdownGames.GameAi.StateMachineAi;
using LockdownGames.Mechanics.ActorMechanics.MovementMechanics;

namespace LockdownGames.GameAi.Enemies.Zombies
{
    public class WanderingState : State<ZombieAi>
    {
        private RigidBodyWanderingMechanics wanderingMechanics;

        public override void SetState(StateMachine sm)
        {
            base.SetState(sm);
            wanderingMechanics = stateMachine.GetComponent<RigidBodyWanderingMechanics>();
        }

        public override void End()
        {
        }

        public override void Start()
        {
        }

        public override void FixedUpdate()
        {
            wanderingMechanics.Wander();
        }

        public override void Update()
        {
            
        }
    }
}

