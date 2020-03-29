using GameAi.Code;
using GameAi.Code.Player;
using GameAi.FiniteStateMachine;
using GameCode.GameAi.Code;
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
        public float detectionDistance;

        [HideInInspector] public const bool HaveCaughtPlayer = true;
        [HideInInspector] public Animator animator;

        protected IList<FieldOfView> _fovs;
        protected Seeker seeker;
        protected PlayerInView playerFound;

        protected Vector2 currentPosition => (Vector2)transform.position;

        protected PlayerInView lastKnownPlayerPosition;

        private new Rigidbody2D rigidbody;
        private IDictionary<string, Transform> detectors;

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
            animator = GetComponent<Animator>();

            playerFound = new PlayerInView();

            rigidbody = GetComponent<Rigidbody2D>();

            LoadDetectors();
        }

        private void LoadDetectors()
        {
            detectors = new Dictionary<string, Transform>();
            foreach (Transform child in transform)
            {
                if (child.tag != "Detectors")
                {
                    continue;
                }

                detectors.Add(child.name, child.transform);
            }
        }

        private bool DetectInDir(Vector2 dir)
        {
            // up, down, left, right
            var blockedDirections = new bool[] { false, false, false, false };

            if (dir.x < -0.5) // left
            {
                RaycastHit2D hitUL = HitWithDebug(detectors["UpLeftDetector"].position, Vector2.left);
                RaycastHit2D hitL =  HitWithDebug(detectors["LeftDetector"].position, Vector2.left);
                RaycastHit2D hitDL = HitWithDebug(detectors["DownLeftDetector"].position, Vector2.left);

                if (hitUL.collider != null || hitL.collider != null || hitDL.collider != null)
                {
                    blockedDirections[2] = true;
                }
            }
            else if (dir.x > 0.5) // right
            {
                RaycastHit2D hitUR = HitWithDebug(detectors["UpRightDetector"].position, Vector2.right);
                RaycastHit2D hitR =  HitWithDebug(detectors["RightDetector"].position, Vector2.right);
                RaycastHit2D hitDR = HitWithDebug(detectors["DownRightDetector"].position, Vector2.right);

                if (hitUR.collider != null || hitR.collider != null || hitDR.collider != null)
                {
                    blockedDirections[3] = true;
                }
            }

            if (dir.y < -0.5) // down
            {
                RaycastHit2D hitDL = HitWithDebug(detectors["DownLeftDetector"].position, Vector2.down);
                RaycastHit2D hitD =  HitWithDebug(detectors["DownDetector"].position, Vector2.down);
                RaycastHit2D hitDR = HitWithDebug(detectors["DownRightDetector"].position, Vector2.down);

                if (hitDL.collider != null || hitD.collider != null || hitDR.collider != null)
                {
                    blockedDirections[1] = true;
                }
            }
            else if (dir.y > 0.5) // up
            {
                RaycastHit2D hitUL = HitWithDebug(detectors["UpLeftDetector"].position, Vector2.up);
                RaycastHit2D hitU =  HitWithDebug(detectors["UpDetector"].position, Vector2.up);
                RaycastHit2D hitUR = HitWithDebug(detectors["UpRightDetector"].position, Vector2.up);

                if (hitUL.collider != null || hitU.collider != null || hitUR.collider != null)
                {
                    blockedDirections[0] = true;
                }
            }

            return blockedDirections[0] 
                | blockedDirections[1]
                | blockedDirections[2]
                | blockedDirections[3];
        }

        private RaycastHit2D HitWithDebug(Vector2 pos, Vector2 dir)
        {

            RaycastHit2D hit = Physics2D.Raycast(pos, dir, detectionDistance, obstacleLayerMask);
            Debug.DrawLine(pos, pos + dir * detectionDistance, Color.red, 0.5f);

            return hit;
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

            // todo: need to use up and down ray projectors for left and right movement
            // and left and right ray projectors for up and down movement
           return DetectInDir(dir);
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
            animator.SetBool("IsRunning", true);
            Move(dir, RunSpeed);
        }

        public void MoveInDirection(Vector2 dir)
        {
            animator.SetBool("IsRunning", false);
            Move(dir, MoveSpeed);
        }

        private void Move(Vector2 dir, float speed)
        {
            Vector2 velocity = dir * speed;// * Time.deltaTime;

            dir.x = dir.x > 0.5 ? 1 : dir.x < -0.5 ? -1 : 0;
            dir.y = dir.y > 0.5 ? 1 : dir.y < -0.5 ? -1 : 0;

            var motionState = GetMotionState((int)dir.x, (int)dir.y);
            animator.SetInteger("MotionState", (int)motionState);

            rigidbody.velocity = velocity;

            foreach (var fov in _fovs)
            {
                fov.UpdateLookDirection(dir);
            }
        }

        private MotionState GetMotionState(int h, int v)
        {
            //Debug.Log("h and v " + h + "  &  " + v);
            if (h == 0 && v == 0)
            {
                return MotionState.Idle;
            }

            if (h != 0)
            {
                return h < 0 ? MotionState.WalkingLeft : MotionState.WalkingRight;
            }

            return v < 0 ? MotionState.WalkingDown : MotionState.WalkingUp;
        }

    }
}