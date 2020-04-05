using GameCode.Models;
using SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomAttributes;
using SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomScripts;
using System.Linq;
using UnityEngine;

namespace SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomAttributeProcessor
{
    public abstract class AttributeProcessorSO : ScriptableObject
    {
        public SizeObject LevelSize;
        protected abstract R Process<R>(LevelData levelData) where R : class;
    }

    public abstract class AttributeProcessor<T> : AttributeProcessorSO where T : RoomAttributeType
    {
        protected T GetRoomAttribute(RoomBuilder roomBuilder)
        {
            var attribute = roomBuilder.roomAttributes?.FirstOrDefault(a => (a as T) != null);
            return attribute == null ? null : attribute as T;
        }
    }
}
