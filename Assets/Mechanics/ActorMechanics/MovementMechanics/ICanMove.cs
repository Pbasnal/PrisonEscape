using UnityEngine;

namespace LockdownGames.Mechanics.ActorMechanics.MovementMechanics
{
    public interface ICanMove
    {
        bool IsMoving { get; }
        void Move(Vector2 target);
    }
}
