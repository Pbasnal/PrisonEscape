using System;
using System.Collections.Generic;
using UnityEngine;

namespace LockdownGames.Mechanics.ActorMechanics.CombatMechanics.TargetApproach
{
    /// <summary>
    /// IApproachTarget will define the path taken by the agent towards the target.
    /// It should also be able to handle more nuances, like variable speed of the 
    /// agent along the path
    /// </summary>
    public abstract class ApproachTarget : MonoBehaviour
    {
        public ApproachData npcApproachData;
        public SpeedMultiplier[] speedMultipliers;

        private List<SpeedMultiplier> _speedMultiplier;

        private void OnDrawGizmosSelected()
        {
            if (npcApproachData == null 
                || npcApproachData.speedGradient == null
                || npcApproachData.speedGradient.alphaKeys== null)
            {
                return;
            }
            //Debug.Log(npcApproachData.gradient.alphaKeys.Length);

            if (_speedMultiplier == null)
            {
                _speedMultiplier = new List<SpeedMultiplier>();
            }

            for (int i = 0; i < npcApproachData.speedGradient.alphaKeys.Length; i++)
            {
                if (_speedMultiplier.Count <= i)
                {
                    _speedMultiplier.Add(new SpeedMultiplier
                    {
                        speedMultiplier = npcApproachData.speedGradient.alphaKeys[i].alpha,
                        distanceMultiplier = npcApproachData.speedGradient.alphaKeys[i].time
                    });
                }
                else
                {
                    _speedMultiplier[i].speedMultiplier = npcApproachData.speedGradient.alphaKeys[i].alpha;
                    _speedMultiplier[i].distanceMultiplier = npcApproachData.speedGradient.alphaKeys[i].time;
                }
            }

            speedMultipliers = _speedMultiplier.ToArray();
        }

        abstract public void Approach(Transform target, float baseSpeed);
        abstract public void EndApproach();
    }


    [Serializable]
    public class SpeedMultiplier
    {
        public float speedMultiplier;
        public float distanceMultiplier;
    }
}
