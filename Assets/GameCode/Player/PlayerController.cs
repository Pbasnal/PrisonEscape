using GameCode.Player;
using UnityEngine;

namespace GameCode.Player
{
    [RequireComponent(typeof(HealthMechanic))]
    [RequireComponent(typeof(UserInputAttack))]
    [RequireComponent(typeof(UserInputMovement))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        private HealthMechanic _healthMechanic;
        private UserInputMovement _userInputMovement;
        private UserInputAttack _userInputAttack;
        private Rigidbody2D _rigidBody;
        private CapsuleCollider2D _collider;

        private void Awake()
        {
            _healthMechanic = GetComponent<HealthMechanic>();
            _rigidBody = GetComponent<Rigidbody2D>();
            _userInputAttack = GetComponent<UserInputAttack>();
            _userInputMovement = GetComponent<UserInputMovement>();
            _collider = GetComponent<CapsuleCollider2D>();

            _healthMechanic.Register(OnHealthUpdate);
        }

        private void OnHealthUpdate(HealthUpdate healthUpdate)
        {
            if (healthUpdate.CurrentHealth != 0)
            {
                return;
            }

            _rigidBody.velocity = Vector2.zero;
            _rigidBody.isKinematic = true;
            _collider.enabled = false;
            _userInputAttack.enabled = false;
            _userInputMovement.enabled = false;

            enabled = false;
        }
    }
}
