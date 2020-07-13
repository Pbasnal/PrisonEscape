using LockdownGames.GameAi.StateMachineAi;
using LockdownGames.Mechanics.ActorMechanics.MovementMechanics;
using LockdownGames.Utilities;

using UnityEngine;

namespace LockdownGames.GameAi.Enemies.Zombies
{
    public class SearchLastKnownPositionState : State<ZombieAi>
    {
        private RigidBodyMovement mover;

        public override void SetState(StateMachine sm)
        {
            base.SetState(sm);
            mover = stateMachine.GetComponent<RigidBodyMovement>();
        }

        public override void End()
        {
            mover.onPathComplete -= OnPathComplete;
        }

        public override void Start()
        {
            mover.SetPathTo(stateMachine.lastKnownPosition);
            mover.onPathComplete += OnPathComplete;
        }

        private void OnPathComplete()
        {
            if (stateMachine.currentState.Hash != this.Hash)
            {
                return;
            }

            stateMachine.SetStateTo<WanderingState>();
        }
        public override void FixedUpdate()
        {
            mover.RunToNextPoint();
            SwitchToWanderingStateIfPathIsBlocked();
        }

        public override void Update()
        {
            
        }

        private void SwitchToWanderingStateIfPathIsBlocked()
        {
            var direction = mover.direction.SnapVectorToAxis();
            var hit = stateMachine.gameObject.GetHitInDirection(stateMachine.boxCastSize, direction, 1, Color.red);
            if (hit.collider == null || hit.distance > 0.7f)
            {
                return;
            }

            stateMachine.SetStateTo<WanderingState>();
        }
    }
}

