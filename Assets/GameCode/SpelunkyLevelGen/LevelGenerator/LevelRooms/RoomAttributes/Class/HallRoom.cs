using UnityEngine;

namespace SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomAttributes
{
    [CreateAssetMenu(fileName = "Hall Room", menuName = "RoomAttributes/Class/Hall", order = 51)]
    public class HallRoom : RoomAttribute<RoomClassType>
    {
        public override bool IsRoomAttributePossible(int enterDirection, int exitDirection)
        {
            return false;
        }
    }
}
