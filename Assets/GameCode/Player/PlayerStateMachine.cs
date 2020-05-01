using System.Collections.Generic;

using LockdownGames.GameCode.GameAi.StateMachine2;
using LockdownGames.GameCode.Messages;
using LockdownGames.GameCode.MessagingFramework;
using LockdownGames.Mechanics.ActorMechanics;
using LockdownGames.Mechanics.ActorMechanics.MovementMechanics;
using LockdownGames.Mechanics.InteractionSystem;

using UnityEngine;

namespace LockdownGames.GameCode.Player
{
    public class PlayerStateMachine : StateMachine
    {
        private EquippedMechanics equippedMechanics;
        private InteractionMechanics interactionMechanics;
        private HealthMechanic _healthMechanic;
        private RigidBodyMovement playerMovement;
        private UserInputAttack _userInputAttack;
        private Rigidbody2D _rigidBody;
        private CapsuleCollider2D _collider;

        private void Awake()
        {
            //_healthMechanic = GetComponent<HealthMechanic>();
            _rigidBody = GetComponent<Rigidbody2D>();
            //_userInputAttack = GetComponent<UserInputAttack>();
            playerMovement = GetComponent<RigidBodyMovement>();
            _collider = GetComponent<CapsuleCollider2D>();
            interactionMechanics = GetComponent<InteractionMechanics>();
            equippedMechanics = GetComponent<EquippedMechanics>();

            var startingState = new IdleState(this);
            var moveState = new MoveState(this);
            var sprintState = new SprintState(this);
            //var interactionstate = new InteractionState(this);

            InitializeStateMachine(new List<IState>
            {
                startingState,
                moveState,
                sprintState,
                //interactionstate
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

            playerMovement.SetPathTo(doubleClickMsg.inputLocationInGameSpace);
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

            playerMovement.SetPathTo(singleClickMsg.inputLocationInGameSpace);
            SetStateTo<MoveState>();

            if (singleClickMsg.ClickedOnAnObject)
            {
                CaptureInteractableIfAny(singleClickMsg.transformOfClickedObject);
            }
        }

        private void CaptureInteractableIfAny(Transform transformOfClickedOnObject)
        {
            interactionMechanics.SetInteractable(transformOfClickedOnObject.GetComponent<Interactable>());
        }
    }
}
