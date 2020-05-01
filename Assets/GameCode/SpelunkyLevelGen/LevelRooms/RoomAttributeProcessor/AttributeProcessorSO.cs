using System.Linq;

using LockdownGames.GameCode.Models;
using LockdownGames.GameCode.SpelunkyLevelGen.LevelRooms.RoomAttributes;
using LockdownGames.GameCode.SpelunkyLevelGen.LevelRooms.RoomScripts;

using UnityEngine;

namespace LockdownGames.GameCode.SpelunkyLevelGen.LevelRooms.RoomAttributeProcessor
{
    public abstract class AttributeProcessorSO : ScriptableObject
    {
        public IntPair LevelSize;
        protected abstract R Process<R>(LevelData levelData) where R : class;
    }

    public abstract class AttributeProcessor<T> : AttributeProcessorSO where T : RoomAttributeSO
    {
        protected T GetRoomAttribute(RoomBuilder roomBuilder)
        {
            var attribute = roomBuilder.roomAttributes?.FirstOrDefault(a => (a as T) != null);
            return attribute == null ? null : attribute as T;
        }
    }
}
