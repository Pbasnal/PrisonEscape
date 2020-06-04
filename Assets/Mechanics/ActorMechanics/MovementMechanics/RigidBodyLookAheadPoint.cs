using DigitalRubyShared;

using LockdownGames.GameCode.Player;

using UnityEngine;

namespace LockdownGames.Mechanics.ActorMechanics.MovementMechanics
{
    public class RigidBodyLookAheadPoint : MonoBehaviour
    {
        public RigidBodyMovement rigidBodyMover;
        public BasicGestures basicGestures;

        public Animator animator;

        private void Start()
        {
            basicGestures.tapGestureCallback += OnSingleTap;
            //basicGestures.doubleTapGestureCallback += OnDoubleTap;
        }

        private void OnSingleTap(GestureRecognizer arg1, Vector2 arg2, Transform arg3)
        {
            animator.SetTrigger("Clicked");
        }

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, (Vector3)rigidBodyMover.target, Time.deltaTime * 5);
        }
    }
}
