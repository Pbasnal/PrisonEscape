using System;
using System.Collections.Generic;

using DigitalRubyShared;

using LockdownGames.GameAi.StateMachineAi;
using LockdownGames.Mechanics.ActorMechanics;
using LockdownGames.Mechanics.ActorMechanics.MovementMechanics;
using LockdownGames.Mechanics.InteractionSystem;

using UnityEngine;

namespace LockdownGames.GameCode.Player
{
    public class PlayerAi : StateMachine
    {
        public BasicGestures basicGestures;

        [Space]
        [Header("Movement properties")]
        public float walkingSpeed = 80;
        public float runningSpeed = 300;
        public float dashSpeed = 500;
        public float dashDistance = 5;
        public Vector3 target { get; private set; }
        public AStarMover mover { get; private set; }
        public ICanMove movementController { get; private set; }

        public Vector3 currentPosition => transform.position;

        private FollowMechanics followMechanics;
        private InteractionMechanics interactionMechanics;


        private void Awake()
        {
            movementController = GetComponent<ICanMove>();
            mover = GetComponent<AStarMover>();
            interactionMechanics = GetComponent<InteractionMechanics>();
            followMechanics = GetComponent<FollowMechanics>();

            followMechanics.OnFollowTransformDestroyed += OnFollowTransformDestroyed;

            var startingState = new IdleState(this);
            var moveState = new WalkState(this);
            var sprintState = new RunningState(this);
            var followState = new FollowState(this);
            var dashingState = new DashingState(this);

            InitializeStateMachine(new List<IState>
            {
                startingState,
                moveState,
                sprintState,
                followState,
                dashingState
            }, startingState);
        }

        private void OnFollowTransformDestroyed()
        {
            if (currentState.Hash == GetState<FollowState>().Hash)
            {
                SetStateTo<IdleState>();
            }
        }

        private void Start()
        {
            basicGestures.tapGestureCallback += OnSingleTap;
            basicGestures.doubleTapGestureCallback += OnDoubleTap;
            basicGestures.swipeGestureCallback += OnSwipe;
        }

        private void OnSwipe(GestureRecognizer gesture, Vector2 worldFocusPoint, Transform clickedOn)
        {
            var startPoint = Camera.main.ScreenToWorldPoint(new Vector2(gesture.StartFocusX, gesture.StartFocusY));
            var endPoint = Camera.main.ScreenToWorldPoint(
                new Vector2(gesture.StartFocusX + gesture.DeltaX,
                            gesture.StartFocusY + gesture.DeltaY));

            var direction = (endPoint - startPoint).normalized;
            Debug.DrawLine(startPoint, endPoint, Color.red, 5);
            target = transform.position + direction * dashDistance;

            SetStateTo<DashingState>();
        }

        private void OnDoubleTap(GestureRecognizer gesture, Vector2 worldFocusPoint, Transform clickedOn)
        {
            target = worldFocusPoint;

            if (clickedOn == null)
            {
                SetStateTo<RunningState>();
            }
            else
            {
                CaptureInteractableIfAny(clickedOn);
                SetStateTo<FollowState>();
            }
        }

        private void OnSingleTap(GestureRecognizer gesture, Vector2 worldFocusPoint, Transform clickedOn)
        {
            target = worldFocusPoint;

            if (clickedOn == null)
            {
                SetStateTo<WalkState>();
            }
            else
            {
                CaptureInteractableIfAny(clickedOn);
                SetStateTo<FollowState>();
            }
        }

        private void Update()
        {
            currentState.Update();
        }

        private void CaptureInteractableIfAny(Transform transformOfClickedOnObject)
        {
            var interactable = transformOfClickedOnObject.GetComponent<Interactable>();
            if (interactable == null)
            {
                return;
            }
            interactionMechanics.SetInteractable(interactable);
        }
    }
}
