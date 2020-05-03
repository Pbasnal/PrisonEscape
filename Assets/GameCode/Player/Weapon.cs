using System.Collections.Generic;

using LockdownGames.GameCode.AnimatorHandlers;
using LockdownGames.Mechanics.ActorMechanics.CombatMechanics.DealDamageMechanics;
using LockdownGames.Mechanics.ActorMechanics.CombatMechanics.TakeDamageMechanic;

using UnityEngine;

namespace LockdownGames.GameCode.Player
{
    public abstract class Weapon : AnimationClipOverrides, ICanAttack
    {
        [SerializeField] protected float AttackDamage;
        [SerializeField] protected float AttackRange;

        protected GameObject weaponOwner;
        protected float TotalDamage;
        protected abstract Vector2 WeaponCenter { get; }

        public bool IsTargetInRange(Vector2 target)
        {
            return Vector2.Distance(WeaponCenter, target) < AttackRange;
        }

        public void AddAttackModifiers(List<float> modifiers)
        {
            TotalDamage = 0;
            modifiers?.ForEach(m => TotalDamage += m);
            TotalDamage += AttackDamage;
        }

        public abstract void Attack(ICanTakeDamage target);

        public void RemoveWeaponOwner()
        {
            weaponOwner = null;
        }

        public override void ActivateEquipmentOn(MonoBehaviour behaviour)
        {
            weaponOwner = behaviour.gameObject;

            var playerAnimator = behaviour.GetComponent<Animator>();

            if (playerAnimator == null)
            {
                Debug.Log("Player does not have an animator component");
                return;
            }

            OverrideAnimationClips(playerAnimator);
        }

        public override void DeactivateEquipment()
        {
            weaponOwner = null;
            Revert();
        }
    }
}
