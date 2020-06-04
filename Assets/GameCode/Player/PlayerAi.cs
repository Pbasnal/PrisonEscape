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

        private FollowMechanics followMechanics;
        private InteractionMechanics interactionMechanics;
        private RigidBodyMovement mover;

        private void Awake()
        {
            mover = GetComponent<RigidBodyMovement>();
            interactionMechanics = GetComponent<InteractionMechanics>();
            followMechanics = GetComponent<FollowMechanics>();

            followMechanics.OnFollowTransformDestroyed += OnFollowTransformDestroyed;

            var startingState = new IdleState(this);
            var moveState = new WalkState(this);
            var sprintState = new SprintState(this);
            var followState = new FollowState(this);

            InitializeStateMachine(new List<IState>
            {
                startingState,
                moveState,
                sprintState,
                followState
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
        }

        private void OnDoubleTap(GestureRecognizer gesture, Vector2 worldFocusPoint, Transform clickedOn)
        {
            mover.SetPathTo(worldFocusPoint);

            if (clickedOn == null)
            {
                SetStateTo<SprintState>();
            }
            else
            {
                CaptureInteractableIfAny(clickedOn);
                SetStateTo<FollowState>();
            }
        }

        private void OnSingleTap(GestureRecognizer gesture, Vector2 worldFocusPoint, Transform clickedOn)
        {
            mover.SetPathTo(worldFocusPoint);
            
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
