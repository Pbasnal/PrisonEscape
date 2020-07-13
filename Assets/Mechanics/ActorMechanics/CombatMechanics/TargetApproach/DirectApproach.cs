using System;
using Pathfinding;
using UnityEngine;

namespace LockdownGames.Mechanics.ActorMechanics.CombatMechanics.TargetApproach
{
    [Serializable]
    [RequireComponent(typeof(AIPath))]
    [RequireComponent(typeof(AIDestinationSetter))]
    public class DirectApproach : ApproachTarget
    {
        public AIPath aiPath;
        public AIDestinationSetter aiDestinationSetter;

        private void Awake()
        {
            aiPath = GetComponent<AIPath>();
            aiDestinationSetter = GetComponent<AIDestinationSetter>();
        }

        public override void Approach(Transform target, float baseSpeed)
        {
            aiDestinationSetter.target = target;
            aiPath.maxSpeed = baseSpeed;
        }

        public override void EndApproach()
        {
            aiDestinationSetter.target = null;
        }
    }
}