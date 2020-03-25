using GameAi.Code;
using GameAi.Code.Player;
using GameAi.FiniteStateMachine;
using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

namespace GameAi.ZombieStates
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Seeker))]
    public class ZombieStateMachine : StateMachine
    {
        public Path path;
        public LayerMask obstacleLayerMask;
        public int Health;
        public int AttackDamage;
        public float MoveSpeed;
        public float RunSpeed;

        [HideInInspector] public const bool HaveCaughtPlayer = true;

        protected IList<FieldOfView> _fovs;
        protected Seeker seeker;
        protected PlayerInView playerFound;

        protected Vector2 currentPosition => (Vector2)transform.position;

        protected PlayerInView lastKnownPlayerPosition;
        private new Rigidbody2D rigidbody;

        public bool GetPathToPlayer(OnPathDelegate onPathComplete)
        {
            return GetPathTo(playerFound.PlayerTransform.position, onPathComplete);
        }

        public bool GetPathToLastKnownPlayerPosition(OnPathDelegate onPathComplete)
        {
            return GetPathTo(lastKnownPlayerPosition.PlayerTransform.position, onPathComplete);
        }

        protected void Initialize()
        {
            seeker = GetComponent<Seeker>();
            _fovs = GetComponents<FieldOfView>();
            playerFound = new PlayerInView();

            rigidbody = GetComponent<Rigidbody2D>();
        }

        public bool AttackPlayer()
        {
            if (Vector2.Distance(currentPosition, (Vector2)playerFound.PlayerTransform.position) > 1.3f)
            {
                return !HaveCaughtPlayer;
            }

            var reverseDamage = playerFound.PlayerTransform.GetComponent<Player>().TakeDamage(AttackDamage);

            Health -= reverseDamage;

            return HaveCaughtPlayer;
        }

        public bool GetPathTo(Vector2 target, OnPathDelegate OnPathComplete)
        {
            if (Vector2.Distance(currentPosition, target) <= 0.9f)
            {
                return HaveCaughtPlayer;
            }

            if (seeker.IsDone())
            {
                seeker.StartPath(transform.position, target, OnPathComplete);
            }

            return !HaveCaughtPlayer;
        }

        public bool PathIsBlocked(Vector2 target)
        {
            var pos = (Vector2)transform.position;
            var dir = (target - pos).normalized;

            RaycastHit2D hit = Physics2D.Raycast(pos, dir, 0.5f, obstacleLayerMask);
            Debug.DrawRay(pos, dir, Color.red);

            return hit.collider != null;
        }

        public bool IsPlayerInView()
        {
            var playersInView = new PlayerInView();

            playerFound.ClearPlayer();
            foreach (var fov in _fovs)
            {
                var x = fov.FindPlayer();
                if (x.IsPlayersInView)
                {
                    lastKnownPlayerPosition = x;
                    playerFound = x;
                    break;
                }
            }

            return playerFound.IsPlayersInView;
        }

        public void RunInDirection(Vector2 dir)
        {
            Move(dir, RunSpeed);
        }

        public void MoveInDirection(Vector2 dir)
        {
            Move(dir, MoveSpeed);
        }

        private void Move(Vector2 dir, float speed)
        {
            Vector2 velocity = dir * speed;// * Time.deltaTime;
            //transform.position = new Vector2(pos.x, pos.y);

            rigidbody.velocity = velocity;

            foreach (var fov in _fovs)
            {
                fov.UpdateLookDirection(dir);
            }
        }
    }
}