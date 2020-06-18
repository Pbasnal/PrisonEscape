using System.Collections.Generic;
using LockdownGames.GameAi.Enemies.PlantBoss.States;
using LockdownGames.GameAi.StateMachineAi;
using LockdownGames.Mechanics.AiMechanics;

using UnityEngine;

namespace LockdownGames.GameAi.Enemies.PlantBoss
{
    [RequireComponent(typeof(FieldOfView))]
    public class PlantBossAi : StateMachine
    {
        public string CurrentState;
        public Vector2 boxCastSize;

        [Space]
        public FieldOfView fieldOfView;

        [Space]
        public Transform target;
        public Vector2 lastKnownPosition;

        private void Awake()
        {
            var startingState = new IdleSate(this);
            InitializeStateMachine(new List<IState>
            {
                startingState,
                new AttackState(this)
            }, startingState);
        }

        private void Update()
        {
            currentState.Update();
        }

        internal Transform FindPlayerInFov()
        {
            var targetInView = fieldOfView.FindTarget();
            if (!targetInView.isTargetInView)
            {
                return null;
            }

            return targetInView.target;
        }

    }
}
