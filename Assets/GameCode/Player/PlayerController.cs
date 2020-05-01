using System.Collections.Generic;

using LockdownGames.GameAi.StateMachineAi;
using LockdownGames.GameCode.Messages;
using LockdownGames.GameCode.MessagingFramework;
using LockdownGames.Mechanics.ActorMechanics;
using LockdownGames.Mechanics.ActorMechanics.MovementMechanics;
using LockdownGames.Mechanics.InteractionSystem;

using UnityEngine;

namespace LockdownGames.GameCode.Player
{
    public class PlayerController : StateMachine
    {
        private InteractionMechanics interactionMechanics;
        private RigidBodyMovement mover;
        
        private void Awake()
        {
            mover = GetComponent<RigidBodyMovement>();
            interactionMechanics = GetComponent<InteractionMechanics>();

            var startingState = new IdleState(this);
            var moveState = new MoveState(this);
            var sprintState = new SprintState(this);
            
            InitializeStateMachine(new List<IState>
            {
                startingState,
                moveState,
                sprintState
            }, startingState);

            MessageBus.Register<UserInputBeganMessage>(OnSingleClick);
            MessageBus.Register<UserInputDoubleClickMessage>(OnDoubleClick);
        }

        private void Update()
        {
            currentState.Update();
        }

        private void OnDisable()
        {
            MessageBus.Remove<UserInputBeganMessage>(OnSingleClick);
            MessageBus.Remove<UserInputDoubleClickMessage>(OnDoubleClick);
        }

        private void OnDoubleClick(TransportMessage trmsg)
        {
            var doubleClickMsg = trmsg.ConvertTo<UserInputDoubleClickMessage>();
            if (doubleClickMsg == null)
            {
                return;
            }

            mover.SetPathTo(doubleClickMsg.inputLocationInGameSpace);
            SetStateTo<SprintState>();

            if (doubleClickMsg.ClickedOnAnObject)
            {
                CaptureInteractableIfAny(doubleClickMsg.transformOfClickedObject);
            }
        }

        private void OnSingleClick(TransportMessage trmsg)
        {
            var singleClickMsg = trmsg.ConvertTo<UserInputBeganMessage>();
            if (singleClickMsg == null)
            {
                return;
            }

            mover.SetPathTo(singleClickMsg.inputLocationInGameSpace);
            SetStateTo<MoveState>();

            if (singleClickMsg.ClickedOnAnObject)
            {
                CaptureInteractableIfAny(singleClickMsg.transformOfClickedObject);
            }
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
