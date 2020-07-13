using UnityEngine;

namespace LockdownGames.Mechanics.ActorMechanics.CombatMechanics.TargetApproach
{
    public class ApproachData : ScriptableObject
    {
        public float detectionDistance;

        [Space]
        [Header("Approach Speed Multipliers")]
        public Gradient speedGradient;
    }
}
