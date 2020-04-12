using UnityEngine;

namespace GameCode.InteractionSystem
{
    public class Condition : ScriptableObject
    {
        public string Description;
        public bool IsSatisfied;
        public int Hash;
    }
}
