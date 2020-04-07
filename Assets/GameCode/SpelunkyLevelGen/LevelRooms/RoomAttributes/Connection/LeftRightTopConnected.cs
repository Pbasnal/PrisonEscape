using UnityEngine;

namespace SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomAttributes
{
    [CreateAssetMenu(fileName = "LRT Room", menuName = "RoomAttributes/Connectedness/LRT", order = 51)]
    public class LeftRightTopConnected : RoomConnectionAttribute
    {
        public override bool IsRoomAttributePossible(int enterDirection, int exitDirection)
        {
            return false;
        }
    }
}
