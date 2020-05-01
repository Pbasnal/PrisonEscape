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
