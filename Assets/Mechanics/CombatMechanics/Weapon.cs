using System.Collections.Generic;
using LockdownGames.GameAi.StateMachineAi;
using LockdownGames.Mechanics.ActorMechanics.CombatMechanics.DealDamageMechanics;
using LockdownGames.Mechanics.ActorMechanics.CombatMechanics.TakeDamageMechanic;

using UnityEngine;

namespace LockdownGames.Mechanics.CombatMechanics
{
    public class Weapon : StateMachine, ICanAttack
    {
        public float DamageAmount;

        public HealthMechanic canTakDamageTest;

        public string CurrentState = "";

        [Space(3)]
        public WeaponAttack[] attacks;

        public Animator animator;

        private void Start()
        {
            var startingState = new NotAttackingState();
            InitializeStateMachine(new List<IState> { 
                startingState, 
                new AttackState()
            }, startingState);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && currentState != GetState<AttackState>())
            {
                SetStateTo<AttackState>();
            }

            CurrentState = currentState.GetType().Name;
            currentState.Update();
        }

        public void TriggerContactAnimation(int animationState)
        {
            Debug.Log("triggering animation " + animationState);
            animator.SetTrigger("SwitchAnimation");
            animator.SetInteger("AnimationState", animationState);
        }

        public void EndAttackAnimation()
        {
            animator.SetInteger("AnimationState", 0);
        }

        public void Attack(ICanTakeDamage canTakeDamage)
        {
            //attacks[0].Attack(canTakeDamage, DamageAmount);
        }
    }
}
