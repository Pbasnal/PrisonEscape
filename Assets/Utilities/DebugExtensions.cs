using UnityEngine;

namespace LockdownGames.Utilities
{
    public static class DebugExtensions
    {
        public static void DrawBoxCastOnHit(this RaycastHit2D hit, 
            Vector2 origin, Vector2 boxSize, 
            float rotationAlongZ, Color color)
        {
            if (hit.collider == null)
            {
                return;
            }

            DrawBox(hit.point, boxSize, Quaternion.Euler(0, 0, rotationAlongZ), color, 0.1f);
            Debug.DrawLine(origin, hit.point, color);
        }

        public static void DrawBoxCastOnHit(this RaycastHit2D hit, 
            Vector2 origin, Vector2 boxSize, 
            float rotationAlongZ, Color color,
            float duration)
        {
            if (hit.collider == null)
            {
                return;
            }

            DrawBox(hit.point, boxSize, Quaternion.Euler(0, 0, rotationAlongZ), color, duration);
            Debug.DrawLine(origin, hit.point, color, duration);
        }

        public static Box GetBox(Vector2 origin, Vector2 size, Quaternion orientation)
        {
            return new Box(origin, size, orientation);
        }

        public static void DrawBox(Vector2 origin, Vector2 size, Quaternion orientation, Color color, float duration)
        {
            DrawBox(new Box(origin, size, orientation), color, duration);
        }

        public static void DrawBox(Box box, Color color, float duration)
        {
            Debug.DrawLine(box.topLeft, box.topRight, color, duration);
            Debug.DrawLine(box.topRight, box.bottomRight, color, duration);
            Debug.DrawLine(box.bottomRight, box.bottomLeft, color, duration);
            Debug.DrawLine(box.bottomLeft, box.topLeft, color, duration);
        }

        public struct Box
        {
            public Vector2 localTopLeft { get; private set; }
            public Vector2 localTopRight { get; private set; }
            public Vector2 localBottomLeft { get; private set; }
            public Vector2 localBottomRight { get; private set; }

            public Vector2 topLeft { get { return localTopLeft + origin; } }
            public Vector2 topRight { get { return localTopRight + origin; } }
            public Vector2 bottomLeft { get { return localBottomLeft + origin; } }
            public Vector2 bottomRight { get { return localBottomRight + origin; } }

            public Vector2 origin { get; private set; }

            public Box(Vector2 origin, Vector2 halfExtents, Quaternion orientation) : this(origin, halfExtents)
            {
                Rotate(orientation);
            }

            public Box(Vector2 origin, Vector2 size)
            {
                size = size / 2;
                this.localTopLeft = new Vector2(-size.x, size.y);
                this.localTopRight = new Vector2(size.x, size.y);
                this.localBottomLeft = new Vector2(-size.x, -size.y);
                this.localBottomRight = new Vector2(size.x, -size.y);

                this.origin = origin;
            }


            public void Rotate(Quaternion orientation)
            {
                localTopLeft = RotatePointAroundPivot(localTopLeft, Vector2.zero, orientation);
                localTopRight = RotatePointAroundPivot(localTopRight, Vector2.zero, orientation);
                localBottomLeft = RotatePointAroundPivot(localBottomLeft, Vector2.zero, orientation);
                localBottomRight = RotatePointAroundPivot(localBottomRight, Vector2.zero, orientation);
            }
        }

        //This should work for all cast types
        static Vector2 CastCenterOnCollision(Vector2 origin, Vector2 direction, float hitInfoDistance)
        {
            return origin + (direction.normalized * hitInfoDistance);
        }

        static Vector2 RotatePointAroundPivot(Vector2 point, Vector2 pivot, Quaternion rotation)
        {
            Vector2 direction = point - pivot;
            return pivot + (Vector2)(rotation * direction);
        }
    }
}
