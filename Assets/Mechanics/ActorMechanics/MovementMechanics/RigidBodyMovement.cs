using System;
using System.Collections.Generic;
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
        [HideInInspector] public Vector2 direction { get; private set; }

        public Action onPathComplete;

        private List<Vector3> path;
        private Vector2 _position;


        private Seeker seeker;
        private Rigidbody2D rigidBody;

        private void Awake()
        {
            seeker = GetComponent<Seeker>();
            rigidBody = GetComponent<Rigidbody2D>();

            direction = Vector2.zero;
            rigidBody.gravityScale = 0;
            path = new List<Vector3>();
        }

        private void Start()
        {
            Debug.Log(direction);
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

            path.RemoveAt(0);
            if (path.Count == 0)
            {
                onPathComplete?.Invoke();
                direction = rigidBody.velocity = Vector2.zero;
                IsMoving = false;
            }

            return false;
        }

        public void Move(Vector2 target, float speed)
        {
            direction = (target - _position).normalized;
            rigidBody.velocity = direction * speed * Time.deltaTime;
        }

        public void ResetPath()
        {
            path.Clear();
        }

        public bool SetPathTo(Vector2 target)
        {
            if (!seeker.IsDone())
            {
                return false;
            }

            IsMoving = true;
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
            path.RemoveAt(0);
        }
    }
}
