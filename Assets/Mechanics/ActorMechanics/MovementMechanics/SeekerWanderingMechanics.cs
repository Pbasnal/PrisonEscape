using System;
using LockdownGames.Mechanics.ActorMechanics.CombatMechanics;
using LockdownGames.Utilities;
using Pathfinding;
using UnityEngine;

namespace LockdownGames.Mechanics.ActorMechanics.MovementMechanics
{
    public class SeekerWanderingMechanics : MonoBehaviour
    {
        public NpcStats npcStats;
        public Transform wanderingTarget;

        [Space]
        [Header("Debug Info")]
        public Color directionCheckColor;
        public Color hitCheckColor;

        public float distance;
        public Vector2 boxCastSize;

        public DebugState debugState;

        private Vector2[] directions;
        private int currentUpdateIndex = 0;

        private AIPath aiPath;
        private AIDestinationSetter aiDestinationSetter;

        private Vector3 wanderTargetLocation;
        private void Awake()
        {
            aiPath = GetComponent<AIPath>();
            aiDestinationSetter = GetComponent<AIDestinationSetter>();

            debugState = new DebugState();
            directions = new Vector2[]
                {
                     Vector2.left ,
                     Vector2.right,
                     Vector2.up,
                     Vector2.down
                };
        }

        private void Start()
        {
            aiDestinationSetter.target = wanderingTarget;
            aiPath.maxSpeed = npcStats.baseSpeed;
            MoveInRandomDirection();
        }

        public void Wander()
        {
            distance = Vector3.Distance(wanderingTarget.position, transform.position);

            if (distance < 0.7f)
            {
                MoveInRandomDirection();
            }

            wanderingTarget.position = wanderTargetLocation;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            MoveInRandomDirection();
        }

        private void MoveInRandomDirection()
        {
            var direction = GetRandomDirection();
            debugState.movingInDirection = direction;
            SetFarthestPointAsTarget(direction);
        }

        private void SetFarthestPointAsTarget(Vector2 direction)
        {
            var hit = gameObject.GetHitInDirection(boxCastSize, direction, 1, directionCheckColor);

            if (hit.collider == null)
            {
                wanderingTarget.position = direction * 3;
                return;
            }

            wanderingTarget.position = hit.point * UnityEngine.Random.Range(0.2f, 1);
            debugState.nextTarget = wanderingTarget.position;
            wanderTargetLocation = wanderingTarget.position;
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
