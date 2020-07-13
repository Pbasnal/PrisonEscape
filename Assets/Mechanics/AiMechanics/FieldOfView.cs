using UnityEngine;

namespace LockdownGames.Mechanics.AiMechanics
{
    public class FieldOfView : MonoBehaviour
    {
        public string ViewTag;
        public float ViewRadius;
        public LayerMask PlayerMask;
        public LayerMask ObstacleMask;
        public float ViewAngle;

        public Color EditorColor;

        //[HideInInspector]
        public Vector2 LookDirection;

        private void Start()
        {
            if (string.IsNullOrWhiteSpace(ViewTag))
            {
                throw new System.Exception("tag is not set for the the Field of view");
            }
        }

        public bool TryFindTarget(out TargetInView targetInView)
        {
            targetInView = FindTarget();
            return targetInView.isTargetInView;
        }

        public TargetInView FindTarget()
        {
            var playerInView = new TargetInView();

            var playersInRadius = Physics2D.OverlapCircleAll(transform.position, ViewRadius, PlayerMask);

            if (playersInRadius == null || playersInRadius.Length == 0)
            {
                return playerInView;
            }

            foreach (var playerInRadius in playersInRadius)
            {
                var playerDistance = (Vector2)(playerInRadius.transform.position - transform.position);
                var playerDirection = playerDistance.normalized;

                if (Vector2.Angle(LookDirection, playerDirection) >= ViewAngle / 2)
                {
                    continue;
                }

                var distToPlayer = Vector2.Distance(transform.position, playerInRadius.transform.position);
                var obstacleInBetween = Physics2D.Raycast(transform.position, playerDirection, distToPlayer, ObstacleMask);

                if (obstacleInBetween)
                {
                    continue;
                }

                playerInView.AddTargetInfo(playerInRadius.transform, playerDistance.magnitude);
            }

            return playerInView;
        }

        public Vector2 DirectionFromAngle(float angleInDegrees, bool isAngleGlobal = false)
        {
            if (!isAngleGlobal)
            {
                angleInDegrees -= transform.eulerAngles.z;
            }

            return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }

        public void UpdateLookDirection(Vector2 direction)
        {
            LookDirection = new Vector2(direction.x, direction.y);
        }
    }
}
