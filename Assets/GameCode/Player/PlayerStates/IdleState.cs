using GameAi.StateMachine2;
using UnityEngine;

namespace LockdownGames.GameCode.Player
{
    public class IdleState : State<PlayerStateMachine>
    {
        private Animator animator;
        private int motionStateHash;

        public IdleState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            animator = stateMachine.GetComponent<Animator>();
            motionStateHash = Animator.StringToHash("MotionState");
        }

        public override void End()
        {
        }

        public override void Start()
        {
            animator.SetInteger(motionStateHash, 0);
        }

        public override void Update()
        {
        }
    }
}
