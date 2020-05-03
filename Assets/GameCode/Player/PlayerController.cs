using LockdownGames.Mechanics.ActorMechanics.CombatMechanics.TakeDamageMechanic;

using UnityEngine;

namespace LockdownGames.GameCode.Player
{
    public class PlayerController : MonoBehaviour
    {
        public HealthMechanic healthMechanic;
        public PlayerAi playerAi;

        private new Rigidbody2D rigidbody;
        private new Collider2D collider;

        private void Awake()
        {
            playerAi = GetComponent<PlayerAi>();
            rigidbody = GetComponent<Rigidbody2D>();
            collider = GetComponent<Collider2D>();
            healthMechanic = GetComponent<HealthMechanic>();

            healthMechanic.Register(OnHealthUpdate);
        }

        private void OnHealthUpdate(HealthUpdateProperty healthUpdate)
        {
            if (healthUpdate.CurrentHealth != 0)
            {
                return;
            }

            DisablePlayer();
        }

        public void EnablePlayer()
        {
            playerAi.enabled = true;
            rigidbody.isKinematic = true;
            collider.enabled = true;
        }

        public void DisablePlayer()
        {
            playerAi.enabled = false;
            rigidbody.isKinematic = true;
            collider.enabled = false;
        }
    }
}
