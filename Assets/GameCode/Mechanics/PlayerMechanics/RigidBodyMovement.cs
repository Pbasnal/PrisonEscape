using GameCode.Interfaces;
using Pathfinding;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCode.Mechanics.PlayerMechanics
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Seeker))]
    public class RigidBodyMovement : MonoBehaviour, ICanMove
    {
        public float WalkingSpeed = 250.0f;
        public float SprintingSpeed = 400.0f;
        public float NextWaypointDistance = 0.5f;

        [HideInInspector] public float currentSpeed => _rigidBody.velocity.magnitude;

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

            _path = new List<Vector3>();
        }

        // returns wether or not seeker had path and was able to move the object
        public bool WalkToNextPoint()
        {
            if (!_seeker.IsDone())
            {
                return false;
            }

            _moveSpeed = WalkingSpeed;
            MoveToNextWayPoint();

            return true;
        }

        public bool RunToNextPoint()
        {
            if (!_seeker.IsDone())
            {
                return false;
            }

            _moveSpeed = SprintingSpeed;
            MoveToNextWayPoint();

            return true;
        }

        private void MoveToNextWayPoint()
        {
            if (_path.Count == 0)
            {
                return;
            }

            Move(_path[0]);
            _position = transform.position;

            var distanceFromNextPoint = Vector2.Distance(_position, _path[0]);
            if (distanceFromNextPoint > NextWaypointDistance)
            {
                return;
            }

            _path.RemoveAt(0);
            if (_path.Count == 0)
            {
                _rigidBody.velocity = Vector2.zero;
            }
        }

        public void Move(Vector2 target)
        {
            var dir = (target - _position).normalized;

            Vector2 direction;

            if (Mathf.Abs(dir.x) >= Mathf.Abs(dir.y))
            {
                direction = dir.x < 0 ? 
                    SetAnimatorMotionStateLeft() 
                    : SetAnimatorMotionStateRight();
            }
            else
            {
                direction = dir.y < 0 ?
                    SetAnimatorMotionStateDown()
                    : SetAnimatorMotionStateUp();
            }

            

            _rigidBody.velocity = dir * _moveSpeed * Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_path.Count == 0)
            {
                return;
            }

            var dir = ((Vector2)_path[0] - _position).normalized;
            var hit = Physics2D.Raycast(_position, dir, 1f);

            if (hit.collider != null)
            {
                Debug.DrawLine(_position, hit.point, Color.red, 2);

                //Debug.Log(string.Format("Direction: {0}, {1}  - object: {2}",
                //dir.x, dir.y,
                //hit.collider.name));
                //Debug.Log("Can't move forward. Cleaning path");
                _path.Clear();
            }
        }

        private Vector2 SetAnimatorMotionStateDown()
        {
            animator.SetInteger(motionStateHash, 2);

            return Vector2.down;
        }

        private Vector2 SetAnimatorMotionStateUp()
        {
            animator.SetInteger(motionStateHash, 1);

            return Vector2.up;
        }

        private Vector2 SetAnimatorMotionStateLeft()
        {
            animator.SetInteger(motionStateHash, 3);

            return Vector2.left;
        }

        private Vector2 SetAnimatorMotionStateRight()
        {
            animator.SetInteger(motionStateHash, 4);

            return Vector2.right;
        }

        public void SetPathTo(Vector2 target)
        {
            if (!_seeker.IsDone())
            {
                return;
            }

            _seeker.StartPath(transform.position, target, OnPathFound);
        }

        private void OnPathFound(Path p)
        {
            if (p.error)
            {
                return;
            }

            _path = p.vectorPath;
            _path.RemoveAt(0);
        }
    }
}
