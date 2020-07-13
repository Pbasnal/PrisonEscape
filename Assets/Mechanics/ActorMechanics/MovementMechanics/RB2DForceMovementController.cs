
using UnityEngine;

namespace LockdownGames.Mechanics.ActorMechanics.MovementMechanics
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RB2DForceMovementController : ICanMove
    {
        public new Camera camera;

        public float currentSpeed => rigidBody.velocity.magnitude;
        public Vector2 direction { get; private set; }

        public override Vector3 movingToTarget { get; protected set; }

        private Rigidbody2D rigidBody;
        
        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.gravityScale = 0;

            camera = FindCameraElseThrowException();

            direction = Vector2.zero;
        }

        public override void Move(Vector3 target, float speed)
        {
            movingToTarget = target;

            direction = (target - transform.position).normalized;
            rigidBody.AddForce(direction * speed * Time.deltaTime * camera.orthographicSize);
        }

        private Camera FindCameraElseThrowException()
        {
            if (camera != null)
            {
                return camera;
            }

            camera = FindObjectOfType<Camera>();
            if (camera != null)
            {
                return camera;
            }

            throw new UnityException("Add a camera to the scene");
        }
    }
}
