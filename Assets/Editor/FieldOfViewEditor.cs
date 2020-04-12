using GameAi.Code;
using UnityEditor;
using UnityEngine;

namespace EditorScripts
{
    [CustomEditor(typeof(FieldOfView))]
    public class FieldOfViewEditor : Editor
    {
        private void OnSceneGUI()
        {
            // get the chosen game object
            if (target != null)
            {
                var fieldOfview = target as FieldOfView;
                if (fieldOfview.ViewRadius != 0)
                {
                    DrawFieldOfView(fieldOfview);
                }
            }
        }

        private void DrawFieldOfView(FieldOfView fieldOfView)
        {
            Vector2 center = fieldOfView.transform.position;

            Handles.color = fieldOfView.EditorColor;

            var angle = Vector3.Angle(Vector2.up, fieldOfView.LookDirection);

            if (fieldOfView.LookDirection.x < 0)
            {
                angle *= fieldOfView.LookDirection.x;
            }

            var dirA = fieldOfView.DirectionFromAngle(fieldOfView.ViewAngle / 2 + angle);
            var dirB = fieldOfView.DirectionFromAngle(-fieldOfView.ViewAngle / 2 + angle);

            Handles.DrawLine(center, center + dirA * fieldOfView.ViewRadius);
            Handles.DrawLine(center, center + dirB * fieldOfView.ViewRadius);

            Handles.DrawWireDisc(fieldOfView.transform.position // position
                                           , Vector3.forward //normal
                                           , fieldOfView.ViewRadius); // radius
        }
    }
}