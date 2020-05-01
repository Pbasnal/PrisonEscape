using UnityEngine;

namespace LockdownGames.Mechanics.AiMechanics
{
    public class TargetInView
    {
        public bool isTargetInView { get; private set; }
        public Transform target { get; private set; }
        public float distanceFromTarget { get; private set; }

        public TargetInView()
        {
            isTargetInView = false;
        }

        public void AddTargetInfo(Transform targetTransform, float distance)
        {
            isTargetInView = true;
            target = targetTransform;
            distanceFromTarget = distance;
        }

        public void ClearTarget()
        {
            isTargetInView = false;
        }
    }
}
