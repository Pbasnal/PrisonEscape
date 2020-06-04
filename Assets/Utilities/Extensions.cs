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
            var absx = Mathf.Abs(dir.x);
            var absy = Mathf.Abs(dir.y);

            if (absx < 0.1f && absy < 0.1f)
            {
                return Vector2.zero;
            }

            var signx = Mathf.Sign(dir.x);
            var signy = Mathf.Sign(dir.y);

            if (absx - absy > 0.1f)
            {
                return Vector2.right * signx;
            }
            else if (absy - absx > 0.1f)
            {
                return Vector2.up * signy;
            }
            
            return Vector2.right * signx + Vector2.up * signy;
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


