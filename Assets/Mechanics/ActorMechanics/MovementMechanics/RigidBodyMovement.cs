using System;
using System.Collections.Generic;
using LockdownGames.Assets.GameCode;
using Pathfinding;

using UnityEngine;

namespace LockdownGames.Mechanics.ActorMechanics.MovementMechanics
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Seeker))]
    public class RigidBodyMovement : MonoBehaviour, ICanMove
    {
        public float WalkingSpeed = 250.0f;
        public float SprintingSpeed = 400.0f;
        public float NextWaypointDistance = 0.5f;

        public bool IsMoving { get; private set; }

        [HideInInspector] public float currentSpeed => rigidBody.velocity.magnitude;
        public Vector2 direction { get; private set; }
        public Vector2 target { get; private set; }
        public Action onPathComplete;

        private List<Vector3> path;
        private Vector2 _position;

        private new Camera camera;
        private Seeker seeker;
        private Rigidbody2D rigidBody;

        private UnityLogger logger;

        private void Awake()
        {
            seeker = GetComponent<Seeker>();
            rigidBody = GetComponent<Rigidbody2D>();

            if (camera == null)
            {
                camera = FindObjectOfType<Camera>();
            }
            logger = FindObjectOfType<UnityLogger>();

            target = transform.position;
            direction = Vector2.zero;
            rigidBody.gravityScale = 0;
            path = new List<Vector3>();
        }

        // returns wether or not seeker had path and was able to move the object
        public bool WalkToNextPoint()
        {
            return MoveToNextWayPoint(WalkingSpeed);
        }

        public bool RunToNextPoint()
        {
            return MoveToNextWayPoint(SprintingSpeed);
        }

        private bool MoveToNextWayPoint(float speed)
        {
            if (path.Count == 0)
            {
                return false;
            }

            _position = transform.position;
            Move(path[0], speed);

            var distanceFromNextPoint = Vector2.Distance(_position, path[0]);
            if (distanceFromNextPoint > NextWaypointDistance)
            {
                return true;
            }

            //logger.LogInfo($"velocity - {rigidBody.velocity.ToString()}, From - {_position.ToString()} to - {transform.position.ToString()}");
            //logger.LogVelocity(_position, rigidBody.velocity);

            //Debug.Break();
            var pathNode = path[0];
            path.RemoveAt(0);
            if (path.Count != 0)
            {
                logger.StorePosition(_position, pathNode, path.Count);
                return false;
            }
            logger.StorePosition(_position, pathNode, path.Count);

            onPathComplete?.Invoke();
            direction = Vector2.zero;
            rigidBody.velocity = Vector2.zero;
            IsMoving = false;
            return false;
        }

        public void Move(Vector2 target, float speed)
        {
            direction = (target - _position).normalized;
            rigidBody.AddForce(direction * speed * Time.deltaTime * camera.orthographicSize);
        }

        public void ResetPath()
        {
            path.Clear();
            rigidBody.velocity = Vector2.zero;
            IsMoving = false;
            direction = Vector2.zero;
        }

        public bool SetPathTo(Vector2 target)
        {
            if (!seeker.IsDone())
            {
                return false;
            }

            logger.ClearStoredData();
            IsMoving = true;
            this.target = target;
            seeker.StartPath(transform.position, target, OnPathFound);
            return true;
        }

        private void OnPathFound(Path p)
        {
            if (p.error)
            {
                IsMoving = false;
                return;
            }

            path = p.vectorPath;
            path.Add(target);
            path.RemoveAt(0);
        }
    }
}
