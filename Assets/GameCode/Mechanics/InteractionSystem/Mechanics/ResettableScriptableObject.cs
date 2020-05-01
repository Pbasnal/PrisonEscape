using UnityEngine;

namespace LockdownGames.GameCode.Mechanics.InteractionSystem.Mechanics
{
    public abstract class ResettableScriptableObject : ScriptableObject
    {
        public abstract void Reset();
    }
}