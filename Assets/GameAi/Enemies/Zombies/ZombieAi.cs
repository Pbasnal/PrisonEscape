using System.Collections.Generic;

using LockdownGames.GameAi.StateMachineAi;
using LockdownGames.Mechanics.ActorMechanics.MovementMechanics;
using LockdownGames.Mechanics.AiMechanics;

using UnityEngine;

namespace LockdownGames.GameAi.Enemies.Zombies
{
    [RequireComponent(typeof(FieldOfView))]
    [RequireComponent(typeof(RigidBodyWanderingMechanics))]
    public class ZombieAi : StateMachine
    {
        public string CurrentState;
        public Vector2 boxCastSize;

        [Space]
        public FieldOfView longRangeFov;
        public FieldOfView shortRangeFov;

        [Space]
        public Transform target;
        public Vector2 lastKnownPosition;

        private RigidBodyMovement mover;

        private void Awake()
        {
            var startingState = new WanderingState();            
            InitializeStateMachine(new List<IState>
            {
                startingState,
                new ChaseState(),
                new SearchLastKnownPositionState()
            }, startingState);

            mover = GetComponent<RigidBodyMovement>();
        }

        private void Update()
        {
            longRangeFov.UpdateLookDirection(mover.direction);
            shortRangeFov.UpdateLookDirection(mover.direction);

            FindPlayerInFov();

            currentState.Update(); 
            CurrentState = currentState.GetType().Name;
        }

        private void FindPlayerInFov()
        {
            var targetInView = longRangeFov.FindTarget();
            if (targetInView.isTargetInView)
            {
                target = targetInView.target;
                lastKnownPosition = target.position;
                // switch to attack player state
                SetStateTo<ChaseState>();
                return;
            }

            targetInView = shortRangeFov.FindTarget();
            if (targetInView.isTargetInView)
            {
                target = targetInView.target;
                lastKnownPosition = target.position;
                // switch to attack player state
                SetStateTo<ChaseState>();
                return;
            }

            target = null;
        }

    }
}
