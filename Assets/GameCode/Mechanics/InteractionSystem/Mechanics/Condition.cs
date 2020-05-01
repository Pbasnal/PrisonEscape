using UnityEngine;

namespace LockdownGames.GameCode.Mechanics.InteractionSystem.Mechanics
{
    public class Condition : ScriptableObject
    {
        public string Description;
        public bool IsSatisfied;
        public int Hash;
    }
}
