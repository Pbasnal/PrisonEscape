using GameCode.Interfaces;
using UnityEngine;

namespace GameCode.Player
{
    // can't really use messaging because a message is tied to the object(as of now)
    // like playerhealthupdate is tied to player.
    // This restricts this health component to be used for different gameobjects
    public class HealthMechanic : CanRaiseEventsBehaviour<HealthUpdate>, ICanTakeDamage
    {
        [SerializeField] protected float MaxHealth;
        [SerializeField] protected float _currentHealth;

        protected HealthUpdate _healthUpdate;

        private void Start()
        {
            _healthUpdate = new HealthUpdate
            {
                MaxHealth = MaxHealth,
                CurrentHealth = MaxHealth,
                DamageTaken = 0
            };
            _currentHealth = MaxHealth;


            RaiseEvent(_healthUpdate);
        }

        public void TakeDamage(float damageAmount)
        {
            if (_currentHealth == 0)
            {
                return;
            }

            //_currentHealth -= damageAmount;

            if (_currentHealth < 1) // should die if it is 0
            {
                _currentHealth = 0; // to prevent it from going negative
            }

            RaiseEvent(_healthUpdate.WithInfo(MaxHealth, _currentHealth, damageAmount));
        }
    }

    public class HealthUpdate : Object
    {
        public float MaxHealth;
        public float CurrentHealth;
        public float DamageTaken;

        public HealthUpdate WithInfo(float max, float current, float damageTaken)
        {
            MaxHealth = max;
            CurrentHealth = current;
            DamageTaken = damageTaken;

            return this;
        }
    }
}
