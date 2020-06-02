namespace LockdownGames.Mechanics.ActorMechanics.CombatMechanics.TakeDamageMechanic
{
    // can't really use messaging because a message is tied to the object(as of now)
    // like playerhealthupdate is tied to player.
    // This restricts this health component to be used for different gameobjects
    public class HealthMechanic : CanRaiseEventsBehaviour<HealthUpdateProperty>, ICanTakeDamage
    {
        public HealthUpdateProperty healthUpdateProperty;

        private void Start()
        {
            healthUpdateProperty.CurrentHealth = healthUpdateProperty.MaxHealth;
            RaiseEvent(healthUpdateProperty);
        }

        public void TakeDamage(float damageAmount)
        {
            if (healthUpdateProperty.CurrentHealth == 0)
            {
                return;
            }

            healthUpdateProperty.CurrentHealth -= damageAmount;

            if (healthUpdateProperty.CurrentHealth < 1) // should die if it is 0
            {
                healthUpdateProperty.CurrentHealth = 0; // to prevent it from going negative
            }

            RaiseEvent(healthUpdateProperty.WithDamage(damageAmount));
        }
    }
}
