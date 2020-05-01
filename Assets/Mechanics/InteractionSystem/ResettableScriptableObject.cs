using UnityEngine;

namespace LockdownGames.Mechanics.InteractionSystem
{
    public abstract class ResettableScriptableObject : ScriptableObject
    {
        public abstract void Reset();
    }
}