using System.Collections.Generic;
using LockdownGames.Mechanics.ActorMechanics.MovementMechanics;
using LockdownGames.Utilities;

using UnityEngine;

namespace LockdownGames.GameCode.AnimatorHandlers.PlayerAnimationHandlers
{
    public class RigidBodyMovementAnimationHandler : MonoBehaviour
    {
        public Animator animator;

        private RigidBodyMovement mover;
        private IDictionary<Vector2, int> motionStates;
        private int prevState;
        private int motionStateHash;

        private void Awake()
        {
            mover = GetComponent<RigidBodyMovement>();
            animator = GetComponent<Animator>();

            motionStates = new Dictionary<Vector2, int>();
            motionStates.Add(Vector2.zero, 0);
            motionStates.Add(Vector2.up, 1);
            motionStates.Add(Vector2.down, 2);
            motionStates.Add(Vector2.left, 3);
            motionStates.Add(Vector2.right, 4);

            motionStateHash = Animator.StringToHash("MotionState");            
        }

        private void Start()
        {
            prevState = 0;
            animator.SetInteger(motionStateHash, prevState);
        }

        private void Update()
        {
            var dir = mover.direction.SnapVectorToAxis();

            if (prevState == motionStates[dir])
            { 
                return; 
            }

            prevState = motionStates[dir];
            animator.SetInteger(motionStateHash, motionStates[dir]);
        }
    }
}
