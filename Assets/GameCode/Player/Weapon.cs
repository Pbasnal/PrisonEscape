using GameCode.AnimationBehaviour;
using GameCode.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace GameCode.Player
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

        public abstract void Attack(Transform target);

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
