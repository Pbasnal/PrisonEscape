using UnityEngine;

namespace GameCode.InteractionSystem
{
    public abstract class ResettableScriptableObject : ScriptableObject
    {
        public abstract void Reset();
    }
}