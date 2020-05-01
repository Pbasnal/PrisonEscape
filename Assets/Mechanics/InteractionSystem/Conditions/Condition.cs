using UnityEngine;

namespace LockdownGames.Mechanics.InteractionSystem.Conditions
{
    public class Condition : ScriptableObject
    {
        public string Description;
        public bool IsSatisfied;
        public int Hash;
    }
}
