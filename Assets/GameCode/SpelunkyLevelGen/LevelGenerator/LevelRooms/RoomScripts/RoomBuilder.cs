using SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomScripts
{
    [RequireComponent(typeof(Grid))]
    public class RoomBuilder : MonoBehaviour
    {
        public SizeObject roomSize;
        public List<RoomAttributeSO> roomAttributes;

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