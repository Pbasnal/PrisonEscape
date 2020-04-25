using GameCode.InventorySystem;
using GameCode.Mechanics.InventorySystem;
using GameCode.Player;
using UnityEngine;

namespace LockdownGames.Assets.GameCode.Player
{
    public class PlayerTest : MonoBehaviour
    {
        public Animator playerAnimator;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerAnimator.SetInteger("MotionState", 3);
                playerAnimator.SetTrigger("Attack");
            }

        }
    }
}
