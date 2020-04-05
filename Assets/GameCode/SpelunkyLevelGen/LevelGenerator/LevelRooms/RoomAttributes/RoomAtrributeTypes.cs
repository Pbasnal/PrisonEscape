using SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomScripts;
using UnityEngine;

namespace SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomAttributes
{

    public abstract class RoomAttributeType : ScriptableObject
    {
        public abstract RoomAttributeSO GetAttributeValue(RoomBuilder room);
    }

    [CreateAssetMenu(fileName = "RoomConnectedness", menuName = "RooomAttributeTypes/Connectedness", order = 51)]
    public class RoomConnectednessType : RoomAttributeType
    {
        public override RoomAttributeSO GetAttributeValue(RoomBuilder room)
        {
            foreach (var attribute in room.roomAttributes)
            {
                if (attribute as RoomAttribute<RoomConnectednessType> == null)
                {
                    continue;
                }

                return attribute;
            }

            return null;
        }
    }

    // Is the room hall, prison, armory etc
    [CreateAssetMenu(fileName = "RoomClass", menuName = "RooomAttributeTypes/Class", order = 51)]
    public class RoomClassType : RoomAttributeType
    {
        public override RoomAttributeSO GetAttributeValue(RoomBuilder room)
        {
            foreach (var attribute in room.roomAttributes)
            {
                if (attribute as RoomAttribute<RoomClassType> == null)
                {
                    continue;
                }

                return attribute;
            }

            return null;
        }
    }

    // Is the room maintained or broken or destroyed
    [CreateAssetMenu(fileName = "RoomCondition", menuName = "RooomAttributeTypes/Condition", order = 51)]
    public class RoomConditionType : RoomAttributeType
    {
        public override RoomAttributeSO GetAttributeValue(RoomBuilder room)
        {
            foreach (var attribute in room.roomAttributes)
            {
                if (attribute as RoomAttribute<RoomConditionType> == null)
                {
                    continue;
                }

                return attribute;
            }

            return null;
        }
    }

    // Is the room special. Like conains final boss, main treasure
    // contains important piece of story
    [CreateAssetMenu(fileName = "RoomSpeciality", menuName = "RooomAttributeTypes/Speciality", order = 51)]
    public class RoomSpecialityType : RoomAttributeType
    {
        public override RoomAttributeSO GetAttributeValue(RoomBuilder room)
        {
            foreach (var attribute in room.roomAttributes)
            {
                if (attribute as RoomAttribute<RoomSpecialityType> == null)
                {
                    continue;
                }

                return attribute;
            }

            return null;
        }
    }
}
