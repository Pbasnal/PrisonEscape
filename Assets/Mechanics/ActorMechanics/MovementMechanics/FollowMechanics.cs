using System;
using DigitalRubyShared;

using LockdownGames.GameCode.Player;

using UnityEngine;

namespace LockdownGames.Mechanics.ActorMechanics.MovementMechanics
{
    public class FollowMechanics : MonoBehaviour
    {
        public BasicGestures basicGestures;

        public bool IsMoving => mover.IsMoving ;
        public Action OnFollowTransformDestroyed;

        private RigidBodyMovement mover;
        private Transform transformToFollow;

        private bool isRunning = false;

        private void Awake()
        {
            mover = GetComponent<RigidBodyMovement>();
        }

        private void Start()
        {
            basicGestures.tapGestureCallback += OnSingleTap;
            basicGestures.doubleTapGestureCallback += OnDoubleTap;
        }

        public void Follow()
        {
            // in case of pick up items, transform would get destroyed
            if (transformToFollow == null)
            {
                mover.ResetPath();
                return;
            }

            if (isRunning)
            {
                mover.RunToNextPoint();
            }
            else
            {
                mover.WalkToNextPoint();
            }

            mover.SetPathTo(transformToFollow.position);
        }

        private void OnDoubleTap(GestureRecognizer gestureRecognizer, Vector2 worldFocusPoint, Transform objectToFollow)
        {
            if (objectToFollow == null)
            {
                return;
            }

            isRunning = true;
            transformToFollow = objectToFollow;
        }

        private void OnSingleTap(GestureRecognizer gestureRecognizer, Vector2 worldFocusPoint, Transform objectToFollow)
        {
            if (objectToFollow == null)
            {
                return;
            }

            isRunning = false;
            transformToFollow = objectToFollow;
        }
    }
}
