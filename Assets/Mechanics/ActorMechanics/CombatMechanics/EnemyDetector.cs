using LockdownGames.Mechanics.ActorMechanics.CombatMechanics.TakeDamageMechanic;
using LockdownGames.Mechanics.AiMechanics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace LockdownGames.Mechanics.ActorMechanics.CombatMechanics
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class EnemyDetector : MonoBehaviour
    {
        [Space]
        public FieldOfView longRangeFov;
        //public FieldOfView shortRangeFov;

        public UnityAction<Transform> foundATarget;
        public UnityAction<Transform> targetLost;

        private float _detectionRadius;
        public float detectionRadius
        {
            get
            {
                return _detectionRadius;
            }
            set
            {
                if (detectionCollider != null)
                {
                    detectionCollider.radius = value;
                }
                else
                {
                    Debug.Log("collider is null");
                }
                _detectionRadius = value;
            }
        }

        private bool isTargetInRange = false;
        private Transform trackingTarget;
        private CircleCollider2D detectionCollider;

        private void Awake()
        {
            detectionCollider = GetComponent<CircleCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (foundATarget == null || collision.GetComponent<ICanTakeDamage>() == null)
            {
                return;
            }

            isTargetInRange = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (targetLost == null || collision.GetComponent<ICanTakeDamage>() == null)
            {
                return;
            }

            isTargetInRange = false;
            targetLost.Invoke(trackingTarget);
            trackingTarget = null;
        }

        public void FindTargetInRange(Rigidbody2D rb)
        {
            if (!isTargetInRange)
            { 
                return; 
            }
            
            longRangeFov.UpdateLookDirection(rb.velocity);
            
            FindTargetInFov();
        }

        private void FindTargetInFov()
        {
            if (longRangeFov.TryFindTarget(out var targetInView))
            {
                trackingTarget = targetInView.target;
                foundATarget.Invoke(trackingTarget);
                return;
            }
        }

        private void OnDrawGizmosSelected()
        {
            detectionCollider = GetComponent<CircleCollider2D>();
            detectionCollider.isTrigger = true;

            longRangeFov.ViewRadius = detectionCollider.radius;
            Handles.color = new Color(1.0f, 0.0f, 0, 0.2f);
            Handles.DrawSolidArc(transform.position, 
                -Vector3.forward, 
                longRangeFov.LookDirection, 
                longRangeFov.ViewAngle / 2,
                longRangeFov.ViewRadius);
            Handles.DrawSolidArc(transform.position,
                -Vector3.forward,
                longRangeFov.LookDirection,
                -longRangeFov.ViewAngle / 2,
                longRangeFov.ViewRadius);
        }
    }
}
