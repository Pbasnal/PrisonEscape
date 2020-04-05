using UnityEngine;

namespace SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomAttributes
{
    [CreateAssetMenu(fileName = "Prison Room", menuName = "RoomAttributes/Class/Prison", order = 51)]
    public class PrisonRoom : RoomAttribute<RoomClassType>
    {
        public override bool IsRoomAttributePossible(int enterDirection, int exitDirection)
        {
            return false;
        }
    }
}
