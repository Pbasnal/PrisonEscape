using UnityEngine;

namespace SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomAttributes
{
    // between different room attributes, we have and condition
    // so the rooms can be used together. But for a type of attribute
    // only one should be allowed
    //[Serializable]
    
    public abstract class RoomAttributeSO : ScriptableObject
    {
        // This method tells wether the room is possible with this attribute or not
        // It's the room's duty to check all it's attributes and say if the room is possible or not
        public abstract bool IsRoomAttributePossible(int enterDirection, int exitDirection);
    }

    public abstract class RoomAttribute<T> : RoomAttributeSO where T : RoomAttributeType
    {
        public virtual void InvokeAttribute(GameObject gameObject)
        { }
    }
}
