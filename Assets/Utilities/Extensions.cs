using System.Collections.Generic;
using UnityEngine;

namespace LockdownGames.Utilities
{
    public static class Extensions
    {
        public static Vector2 Clone(this Vector2 vector)
        {
            return new Vector2(vector.x, vector.y);
        }

        public static Vector2 SnapVectorToAxis(this Vector2 dir)
        {
            if (Mathf.Abs(dir.x) > 0.1)
            {
                return dir.x < 0 ? Vector2.left : Vector2.right;
            }
            else if (Mathf.Abs(dir.y) > 0.1)
            {
                return dir.y < 0 ? Vector2.down : Vector2.up;
            }

            return Vector2.zero;
        }

        public static Queue<T> ToQueue<T>(this IEnumerable<T> enumerable)
        {
            var queue = new Queue<T>();

            foreach (var item in enumerable)
            {
                queue.Enqueue(item);
            }

            return queue;
        }
    }
}
