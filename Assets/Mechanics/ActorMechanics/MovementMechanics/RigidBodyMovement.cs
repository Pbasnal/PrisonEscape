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

        [HideInInspector] public float currentSpeed => _rigidBody.velocity.magnitude;
        [HideInInspector] public Vector2 direction { get; private set; }

        public Action onPathComplete;

        private float _moveSpeed;
        private List<Vector3> _path;
        private Vector2 _position;


        private Seeker _seeker;
        private Rigidbody2D _rigidBody;
        private Animator animator;
        private int motionStateHash;

        private void Awake()
        {
            _seeker = GetComponent<Seeker>();
            _rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            motionStateHash = Animator.StringToHash("MotionState");

            _rigidBody.gravityScale = 0;
            _path = new List<Vector3>();
        }

        // returns wether or not seeker had path and was able to move the object
        public bool WalkToNextPoint()
        {
            _moveSpeed = WalkingSpeed;
            return MoveToNextWayPoint(WalkingSpeed);
        }

        public bool RunToNextPoint()
        {
            _moveSpeed = SprintingSpeed;
            return MoveToNextWayPoint(SprintingSpeed);
        }

        private bool MoveToNextWayPoint(float speed)
        {
            if (_path.Count == 0)
            {
                return false;
            }
            
            _position = transform.position;
            Move(_path[0]);            

            var distanceFromNextPoint = Vector2.Distance(_position, _path[0]);
            if (distanceFromNextPoint > NextWaypointDistance)
            {
                return true;
            }

            _path.RemoveAt(0);
            if (_path.Count == 0)
            {
                onPathComplete?.Invoke();
                _rigidBody.velocity = Vector2.zero;
                IsMoving = false;
            }

            return false;
        }

        public void Move(Vector2 target)
        {
            direction = (target - _position).normalized;
            SetAnimationDirection();

            _rigidBody.velocity = direction * _moveSpeed * Time.deltaTime;
        }

        private void SetAnimationDirection()
        {
            var targetIndex = 2;
            if (_path.Count <= 2)
            {
                targetIndex = _path.Count - 1;
            }

            var dir = ((Vector2)_path[targetIndex] - _position).normalized;

            if (Mathf.Abs(dir.x) > 0.1)
            {
                dir = dir.x < 0 ?
                    SetAnimatorMotionStateLeft()
                    : SetAnimatorMotionStateRight();
            }
            else
            {
                dir = dir.y < 0 ?
                    SetAnimatorMotionStateDown()
                    : SetAnimatorMotionStateUp();
            }
        }

        public void ResetPath()
        {
            _path.Clear();
        }

        private Vector2 SetAnimatorMotionStateUp()
        {
            //animator.SetInteger(motionStateHash, 1);

            return Vector2.up;
        }

        private Vector2 SetAnimatorMotionStateDown()
        {
            //animator.SetInteger(motionStateHash, 2);

            return Vector2.down;
        }

        private Vector2 SetAnimatorMotionStateLeft()
        {
            //animator.SetInteger(motionStateHash, 3);

            return Vector2.left;
        }

        private Vector2 SetAnimatorMotionStateRight()
        {
            //animator.SetInteger(motionStateHash, 4);

            return Vector2.right;
        }

        public bool SetPathTo(Vector2 target)
        {
            if (!_seeker.IsDone())
            {
                return false;
            }

            IsMoving = true;
            _seeker.StartPath(transform.position, target, OnPathFound);
            return true;
        }

        private void OnPathFound(Path p)
        {
            if (p.error)
            {
                IsMoving = false;
                return;
            }

            _path = p.vectorPath;
            _path.RemoveAt(0);
        }
    }
}
