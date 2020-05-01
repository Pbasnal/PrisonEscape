using LockdownGames.GameAi.StateMachineAi;
using LockdownGames.Mechanics.ActorMechanics.MovementMechanics;

namespace LockdownGames.GameAi.Enemies.Zombies
{
    public class WanderingState : State<ZombieAi>
    {
        private RigidBodyWanderingMechanics wanderingMechanics;

        public WanderingState(ZombieAi stateMachine)
            : base(stateMachine)
        {
            wanderingMechanics = stateMachine.GetComponent<RigidBodyWanderingMechanics>();
        }

        public override void End()
        {
        }

        public override void Start()
        {
        }

        public override void Update()
        {
            wanderingMechanics.Wander();
        }
    }
}

