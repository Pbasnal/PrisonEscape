using UnityEngine;

namespace LockdownGames.Mechanics.ActorMechanics.MovementMechanics
{
    public abstract class ICanMove : MonoBehaviour
    {
        public abstract Vector3 movingToTarget { get; protected set; }
        public abstract void Move(Vector3 target, float speed);
    }
}
