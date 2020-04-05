using UnityEngine;

namespace SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomAttributes
{
    [CreateAssetMenu(fileName = "LR Room", menuName = "RoomAttributes/Connectedness/LR", order = 51)]
    public class LeftRightConnected : RoomAttribute<RoomConnectednessType>
    {
        public override bool IsRoomAttributePossible(int enterDirection, int exitDirection)
        {
            return false;
        }
    }
}
