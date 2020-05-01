using LockdownGames.GameCode.GameAi.StateMachine2;
using LockdownGames.Mechanics.ActorMechanics.MovementMechanics;
using LockdownGames.Mechanics.AiMechanics;

namespace LockdownGames.GameAi.Enemies.Zombies
{
    public class WanderingState : State<ZombieStateMachine2>
    {
        private RigidBodyWanderingMechanics wanderingMechanics;

        public WanderingState(ZombieStateMachine2 stateMachine)
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

