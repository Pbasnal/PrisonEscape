using GameCode.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCode.Player
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SwordWeapon : Weapon
    {
        public float AttackInterval;
        public float EnemyDetectRange;
        public Transform AttackRangeOrigin;
        public LayerMask EnemyLayerMask;
        public List<Transform> weaponPositions;

        private float _timeOfLastAttack;
        private IDictionary<string, Transform> swordAttackPositions;
        private WaitForSeconds _attackForSeconds;

        private BoxCollider2D _trigger;

        private const string WeaponPositionTag = "WeaponPositions";
        private const string WeaponPosDown = "WeaponPosDown";
        private const string WeaponPosUp = "WeaponPosUp";
        private const string WeaponPosLeft = "WeaponPosLeft";
        private const string WeaponPosRight = "WeaponPosRight";

        protected override Vector2 WeaponCenter => transform.position;

        public void Awake()
        {
            swordAttackPositions = new Dictionary<string, Transform>();

            foreach (Transform child in weaponPositions)
            {
                if (child.tag != WeaponPositionTag)
                {
                    continue;
                }

                swordAttackPositions.Add(child.name, child);
            }

            _attackForSeconds = new WaitForSeconds(AttackInterval);

            _trigger = GetComponent<BoxCollider2D>();
            _trigger.enabled = false;
            _trigger.isTrigger = true;
        }

        private Vector2 SnapDirectionToAxis(Vector2 direction)
        {
            if (direction.x == 0 && direction.y == 0)
            {
                return Vector2.zero;
            }

            var angle = Vector2.SignedAngle(Vector2.right, direction);
            if (angle > 45 && angle < 135)
            {
                return Vector2.up;
            }
            else if (angle < -45 && angle > -135)
            {
                return Vector2.down;
            }

            var absAngle = Mathf.Abs(angle);
            if (absAngle < 45)
            {
                return Vector2.right;
            }
            else
            {
                return Vector2.left;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(AttackRangeOrigin.position, AttackRange);
        }

        public void SetWeaponPositionAndDirection(Vector2 dir)
        {
            if (dir == Vector2.up)
            {
                transform.SetPositionAndRotation(swordAttackPositions[WeaponPosUp].position,
                    Quaternion.Euler(0, 0, 180));
            }
            else if (dir == Vector2.down)
            {
                transform.SetPositionAndRotation(swordAttackPositions[WeaponPosDown].position,
                    Quaternion.Euler(0, 0, 0));
            }
            else if (dir == Vector2.left)
            {
                transform.SetPositionAndRotation(swordAttackPositions[WeaponPosLeft].position,
                    Quaternion.Euler(0, 0, -90));
            }
            else
            {
                transform.SetPositionAndRotation(swordAttackPositions[WeaponPosRight].position,
                    Quaternion.Euler(0, 0, 90));
            }
        }

        public override void Attack(Transform target)
        {
            if (Time.time - _timeOfLastAttack < AttackInterval)
            {
                return;
            }

            _timeOfLastAttack = Time.time;
            Vector2 directionOfClosestEnemy = target.position - transform.position;

            if (directionOfClosestEnemy == Vector2.zero)
            {
                return;
            }

            SetWeaponPositionAndDirection(SnapDirectionToAxis(directionOfClosestEnemy));
            _trigger.enabled = true;

            // attack will happen if collider collision happens.
            // go to OnTriggerEnter2D method
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            var damagable = collider.GetComponent<ICanTakeDamage>();

            _trigger.enabled = false;

            if (damagable == null)
            {
                return;
            }
            damagable.TakeDamage(TotalDamage);
        }
    }
}
