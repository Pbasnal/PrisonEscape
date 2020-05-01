using System.Collections.Generic;
using System.Linq;
using LockdownGames.Utilities;

using UnityEngine;

namespace LockdownGames.Mechanics.ActorMechanics.MovementMechanics
{
    public class WanderingMechanics : MonoBehaviour
    {
        public RigidBodyMovement mover;
        public Vector2 boxCastSize;

        private IDictionary<Vector2, float> posUpdatematrix;
        private Vector2[] keys;
        private int currentUpdateIndex = 0;

        public Vector2 direction;
        public float rotation;

        private void Awake()
        {
            posUpdatematrix = new Dictionary<Vector2, float>
                {
                    { Vector2.left , 90},
                    { Vector2.right, 90 },
                    { Vector2.up,  0},
                    { Vector2.down, 0}
                };
            keys = posUpdatematrix.Keys.ToArray();

            mover = GetComponent<RigidBodyMovement>();

            if (mover == null)
            {
                Debug.LogError("Wandering Mechanics: Add an ICanMove component to this gameobject");
                this.gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            MoveInRandomDirection();
        }

        private void Update()
        {
            gameObject.GetFarthestPointInDirection(boxCastSize, direction, rotation);
            if (mover.IsMoving)
            {
                mover.WalkToNextPoint();
                return;
            }
            MoveInRandomDirection();
            
        }

        private void MoveInRandomDirection()
        {
            var direction = GetRandomDirection();
            SetFarthestPointAsTarget(direction);
        }

        private Vector2? SetFarthestPointAsTarget(Vector2 direction)
        {
            if (!posUpdatematrix.TryGetValue(direction, out var rotation))
            {
                return null;
            }
            this.direction = direction;
            this.rotation = rotation;

            var hit = gameObject.GetFarthestPointInDirection(boxCastSize, direction, rotation);

            if (hit.collider == null)
            {
                return null;
            }

            mover.SetPathTo(hit.point);

            return hit.point;
        }

        public Vector2 GetRandomDirection()
        {
            currentUpdateIndex = Random.Range(0, 4);
            return keys[currentUpdateIndex];
        }
    }
}
