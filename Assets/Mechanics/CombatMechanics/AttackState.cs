using LockdownGames.GameAi.StateMachineAi;

namespace LockdownGames.Mechanics.CombatMechanics
{
    public class AttackState : State<Weapon>
    {
        //public AttackState(Weapon stateMachine) : base(stateMachine)
        //{
        //}

        public override void End()
        {

        }

        public override void Start()
        {
            stateMachine.attacks[0].ResetTime();
            stateMachine.TriggerContactAnimation(stateMachine.attacks[0].animationState);
        }

        public override void FixedUpdate()
        {
        }

        public override void Update()
        {
            var attackStage = stateMachine.attacks[0].Attack(stateMachine.canTakDamageTest, stateMachine.DamageAmount);

            switch (attackStage)
            {
                case AttackStage.Complete:
                    stateMachine.SetStateTo<NotAttackingState>();
                    break;
            }
        }
    }
}
