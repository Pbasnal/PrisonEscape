using UnityEngine;

namespace SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomAttributes
{
    [CreateAssetMenu(fileName = "LRTB Room", menuName = "RoomAttributes/Connectedness/LRTB", order = 51)]
    public class LeftRightTopBottomConnected : RoomAttribute<RoomConnectednessType>
    {
        public override bool IsRoomAttributePossible(int enterDirection, int exitDirection)
        {
            return true;
        }
    }
}
