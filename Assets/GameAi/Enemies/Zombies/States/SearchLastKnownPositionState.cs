using LockdownGames.GameCode.GameAi.StateMachine2;
using LockdownGames.Mechanics.ActorMechanics.MovementMechanics;
using LockdownGames.Utilities;
using UnityEngine;

namespace LockdownGames.GameAi.Enemies.Zombies
{
    public class SearchLastKnownPositionState : State<ZombieStateMachine2>
    {
        private RigidBodyMovement mover;

        public SearchLastKnownPositionState(ZombieStateMachine2 stateMachine)
            : base(stateMachine)
        {
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

        public override void Update()
        {
            mover.RunToNextPoint();
            SwitchToWanderingStateIfPathIsBlocked();
        }

        private void SwitchToWanderingStateIfPathIsBlocked()
        {
            var direction = GetDirectionInAxis(mover.direction);
            var hit = stateMachine.gameObject.GetHitInDirection(stateMachine.boxCastSize, direction, 1, Color.red);
            if (hit.collider == null || hit.distance > 0.7f)
            {
                return;
            }

            stateMachine.SetStateTo<WanderingState>();
        }

        private Vector2 GetDirectionInAxis(Vector2 dir)
        {
            if (Mathf.Abs(dir.x) > 0.1)
            {
                return dir.x < 0 ? Vector2.left: Vector2.right;
            }
            else
            {
                return dir.y < 0 ? Vector2.down : Vector2.up;
            }
        }
    }
}

