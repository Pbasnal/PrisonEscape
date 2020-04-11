using GameCode.GameAi.Code;
using GameCode.Interfaces;
using GameCode.Messages;
using GameCode.MessagingFramework;
using Pathfinding;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCode.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Seeker))]
    public class UserInputMovement : MonoBehaviour, ICanMove
    {
        public float WalkingSpeed = 1.0f;
        public float SprintingSpeed = 4.0f;
        public float NextWaypointDistance = 0.5f;

        public Animator Animator;
        
        private float _moveSpeed;
        private List<Vector3> _path;
        private Vector2 _position;

        private Seeker _seeker;
        private Rigidbody2D _rigidBody;

        private void Awake()
        {
            _seeker = GetComponent<Seeker>();
            _rigidBody = GetComponent<Rigidbody2D>();

            MessageBus.Register<UserInputBeganMessage>(StartMoving);
            MessageBus.Register<UserInputDoubleClickMessage>(StartRunning);
        }

        private void OnDisable()
        {
            MessageBus.Remove<UserInputBeganMessage>(StartMoving);
            MessageBus.Remove<UserInputDoubleClickMessage>(StartRunning);
        }

        private void Start()
        {
            _path = new List<Vector3>();
        }

        private void FixedUpdate()
        {
            _position = transform.position;

            MoveToNextWayPoint();
            Animator.SetInteger("MotionState", (int)GetMotionState(_rigidBody.velocity));
        }

        private void MoveToNextWayPoint()
        {
            if (_path.Count == 0)
            {
                return;
            }
            Move(_path[0]);

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
            var pos = (Vector2)transform.position;
            var dir = (target - pos).normalized;

            _rigidBody.velocity = dir * _moveSpeed * Time.deltaTime;
        }

        private MotionState GetMotionState(Vector2 velocity)
        {
            if (velocity.x == 0 && velocity.y == 0)
            {
                return MotionState.Idle;
            }

            var angle = Vector2.SignedAngle(Vector2.right, velocity);
            if (angle > 45 && angle < 135)
            {
                return MotionState.WalkingUp;
            }
            else if (angle < -45 && angle > -135)
            {
                return MotionState.WalkingDown;
            }

            var absAngle = Mathf.Abs(angle);
            if (absAngle < 45)
            {
                return MotionState.WalkingRight;
            }
            else
            {
                return MotionState.WalkingLeft;
            }
        }

        private void OnPathFound(Path p)
        {
            if (p.error)
            {
                return;
            }

            _path = p.vectorPath;
            _path.RemoveAt(0);// because index 0 is the position of the seeker
        }

        private void StartRunning(TransportMessage trMsg)
        {
            if (!_seeker.IsDone())
            {
                return;
            }

            var msg = trMsg.ConvertTo<UserInputDoubleClickMessage>();
            _moveSpeed = SprintingSpeed;
            _seeker.StartPath(transform.position, msg.InputLocationInGameSpace, OnPathFound);
        }

        private void StartMoving(TransportMessage trMsg)
        {
            if (!_seeker.IsDone())
            {
                return;
            }

            var msg = trMsg.ConvertTo<UserInputBeganMessage>();
            _moveSpeed = WalkingSpeed;
            _seeker.StartPath(transform.position, msg.InputLocationInGameSpace, OnPathFound);
        }
    }
}
