using System;
using LockdownGames.Utilities;

using UnityEngine;

namespace LockdownGames.Mechanics.ActorMechanics.MovementMechanics
{
    [RequireComponent(typeof(RigidBodyMovement))]
    public class RigidBodyWanderingMechanics : MonoBehaviour
    {
        public Color directionCheckColor;
        public Color hitCheckColor;

        public float distance;
        public Vector2 boxCastSize;

        public DebugState debugState;

        private Vector2 hitLocation;
        private Vector2[] directions;
        private int currentUpdateIndex = 0;

        private Vector2 direction;

        private RigidBodyMovement mover;

        private void Awake()
        {
            debugState = new DebugState();
            directions = new Vector2[]
                {
                     Vector2.left ,
                     Vector2.right,
                     Vector2.up,
                     Vector2.down
                };

            mover = GetComponent<RigidBodyMovement>();

            if (mover == null)
            {
                Debug.LogError("Wandering Mechanics: Add an ICanMove component to this gameobject");
                this.gameObject.SetActive(false);
            }

            hitLocation = transform.position;
        }

        public void Wander()
        {
            distance = (hitLocation - (Vector2)transform.position).magnitude;

            if (distance < 0.7f || !mover.IsMoving)
            {
                mover.ResetPath();
                MoveInRandomDirection();
            }

            mover.WalkToNextPoint();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (direction == Vector2.zero)
            {
                return;
            }

            var hit = gameObject.GetHitInDirection(boxCastSize, direction, 1, hitCheckColor);

            if (hit.collider == null || hit.distance > 0.7f)
            {
                return;
            }

            debugState.CollidedWith = collision.collider.name;
            debugState.CollisionRayHit = hit.collider.name;

            mover.ResetPath();
            MoveInRandomDirection();
        }

        private void MoveInRandomDirection()
        {
            var direction = GetRandomDirection();
            debugState.movingInDirection = direction;
            hitLocation = SetFarthestPointAsTarget(direction);
        }

        private Vector2 SetFarthestPointAsTarget(Vector2 direction)
        {
            this.direction = direction;

            var hit = gameObject.GetHitInDirection(boxCastSize, direction, 1, directionCheckColor);

            if (hit.collider == null)
            {
                return transform.position;
            }

            debugState.seekerFoundPath = mover.SetPathTo(hit.point);

            debugState.nextTarget = hit.point;

            return debugState.seekerFoundPath ? hit.point : (Vector2)transform.position;
        }

        public Vector2 GetRandomDirection()
        {
            currentUpdateIndex = UnityEngine.Random.Range(0, 4);
            return directions[currentUpdateIndex];
        }

        [Serializable]
        public class DebugState
        {
            public string CollidedWith;
            public string CollisionRayHit;
            public bool movingAlongPath;
            public Vector2 movingInDirection;
            public Vector2 nextTarget;
            public bool seekerFoundPath;
        }
    }
}
