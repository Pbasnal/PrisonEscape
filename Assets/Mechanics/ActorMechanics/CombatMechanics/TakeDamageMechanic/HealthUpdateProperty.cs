
using UnityEngine;

namespace LockdownGames.Mechanics.ActorMechanics.CombatMechanics.TakeDamageMechanic
{
    [CreateAssetMenu(fileName = "Health Info", menuName = "Actor/Health", order = 51)]
    public class HealthUpdateProperty : ScriptableObject
    {
        public float MaxHealth;
        [HideInInspector] public float CurrentHealth;
        [HideInInspector] public float DamageTaken;

        public HealthUpdateProperty WithDamage(float damageTaken)
        {
            DamageTaken = damageTaken;

            return this;
        }
    }
}
