using LockdownGames.Mechanics.ActorMechanics.CombatMechanics.TakeDamageMechanic;

namespace LockdownGames.Mechanics.ActorMechanics.CombatMechanics.DealDamageMechanics
{
    public interface ICanAttack
    {
        void Attack(ICanTakeDamage canTakeDamage);
    }
}
