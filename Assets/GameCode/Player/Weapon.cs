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
    }
}
