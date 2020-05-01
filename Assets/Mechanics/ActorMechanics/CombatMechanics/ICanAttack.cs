using UnityEngine;

namespace LockdownGames.Mechanics.ActorMechanics.CombatMechanics
{
    public interface ICanAttack
    {
        void Attack(ICanTakeDamage canTakeDamage);
    }
}
