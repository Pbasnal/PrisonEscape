using UnityEngine;

namespace LockdownGames.GameCode.Player
{
    public class PlayerTest : MonoBehaviour
    {
        public Animator playerAnimator;
        public int motionState;

        private void Update()
        {
            playerAnimator.GetInteger("MotionState");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerAnimator.SetTrigger("Attack");
            }

        }
    }
}
