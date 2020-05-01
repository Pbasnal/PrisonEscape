using UnityEngine;

namespace LockdownGames.Utilities
{
    public static class GameObjectExtensions
    {
        public static RaycastHit2D GetFarthestPointInDirection(this GameObject gameObject, Vector2 boxCastSize, Vector2 direction, float rotation)
        {
            var hit = Physics2D.BoxCast(gameObject.transform.position, boxCastSize, rotation, direction);

            if (hit.collider != null)
            {
                hit.DrawBoxCastOnHit(gameObject.transform.position, boxCastSize, rotation, Color.red);
            }

            return hit;
        }

        public static RaycastHit2D GetFarthestPointInDirection(this GameObject gameObject, Vector2 boxCastSize, Vector2 direction, float rotation, Color color)
        {
            var hit = Physics2D.BoxCast(gameObject.transform.position, boxCastSize, rotation, direction);

            if (hit.collider != null)
            {
                hit.DrawBoxCastOnHit(gameObject.transform.position, boxCastSize, rotation, color);
            }

            return hit;
        }
    }
}
