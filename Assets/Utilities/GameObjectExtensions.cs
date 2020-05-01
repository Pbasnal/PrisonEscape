using System.Collections.Generic;
using UnityEngine;

namespace LockdownGames.Utilities
{
    public static class GameObjectExtensions
    {
        private static IDictionary<Vector2, float> posUpdatematrix;

        static GameObjectExtensions()
        {
            posUpdatematrix = new Dictionary<Vector2, float>
                {
                    { Vector2.left , 90},
                    { Vector2.right, 90 },
                    { Vector2.up,  0},
                    { Vector2.down, 0}
                };
        }

        public static RaycastHit2D GetFarthestPointInDirection(this GameObject gameObject, Vector2 boxCastSize, Vector2 direction)
        {
            var hit = Physics2D.BoxCast(gameObject.transform.position, boxCastSize, posUpdatematrix[direction], direction);

            if (hit.collider != null)
            {
                hit.DrawBoxCastOnHit(gameObject.transform.position, boxCastSize, posUpdatematrix[direction], Color.red);
            }

            return hit;
        }

        public static RaycastHit2D GetFarthestPointInDirection(this GameObject gameObject, Vector2 boxCastSize, Vector2 direction, Color color)
        {
            var hit = Physics2D.BoxCast(gameObject.transform.position, boxCastSize, posUpdatematrix[direction], direction);

            if (hit.collider != null)
            {
                hit.DrawBoxCastOnHit(gameObject.transform.position, boxCastSize, posUpdatematrix[direction], color);
            }

            return hit;
        }

        public static RaycastHit2D GetHitInDirection(this GameObject gameObject,
            Vector2 boxCastSize, Vector2 direction,
            float duration, Color color)
        {
            if (!posUpdatematrix.TryGetValue(direction, out var rotation))
            {
                Debug.LogError($"Doesn't contain this direction: {direction.x}  {direction.y}");
                throw new KeyNotFoundException($"Doesn't contain this direction: {direction.x}  {direction.y}");
            }

            var hit = Physics2D.BoxCast(gameObject.transform.position, boxCastSize, rotation, direction);

            if (hit.collider != null)
            {
                hit.DrawBoxCastOnHit(gameObject.transform.position, boxCastSize, rotation, color, duration);
            }

            return hit;
        }
    }
}
