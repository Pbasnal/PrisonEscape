using SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomScripts
{
    [RequireComponent(typeof(Grid))]
    public class RoomBuilder : MonoBehaviour
    {
        public IntPair roomSize;
        public List<RoomAttributeSO> roomAttributes;

        public RoomConnectionAttribute RoomConnectionAttribute { get; private set; }
        public RoomClassAttribute RoomClassAttribute { get; private set; }
        public List<RoomSpecialAttribute> RoomSpecialAttributes { get; private set; }

        private void Awake()
        {
            RoomSpecialAttributes = new List<RoomSpecialAttribute>();

            foreach (var attr in roomAttributes)
            {
                if (attr is RoomConnectionAttribute) RoomConnectionAttribute = attr as RoomConnectionAttribute;
                else if (attr is RoomClassAttribute) RoomClassAttribute = attr as RoomClassAttribute;
                else if (attr is RoomSpecialAttribute) RoomSpecialAttributes.Add(attr as RoomSpecialAttribute);
            }
        }

        public bool IsRoomPossible(int enterDirection, int exitDirection)
        {
            if (roomAttributes == null || roomAttributes.Count == 0)
            {
                return false;
            }

            var anAttributeExistsWhichIsNotPossible = roomAttributes.Any(a => !a.IsRoomAttributePossible(enterDirection, exitDirection));

            return !anAttributeExistsWhichIsNotPossible;
        }

        public bool HasAllAttributes(List<RoomAttributeSO> attributes)
        {
            var attributesNotPresent = attributes.Except(roomAttributes);

            return attributesNotPresent == null || attributesNotPresent.Count() == 0;
        }
    }
}