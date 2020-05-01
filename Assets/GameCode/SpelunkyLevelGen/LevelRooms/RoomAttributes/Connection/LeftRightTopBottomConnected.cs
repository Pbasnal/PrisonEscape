using UnityEngine;

namespace LockdownGames.GameCode.SpelunkyLevelGen.LevelRooms.RoomAttributes
{
    [CreateAssetMenu(fileName = "LRTB Room", menuName = "RoomAttributes/Connectedness/LRTB", order = 51)]
    public class LeftRightTopBottomConnected : RoomConnectionAttribute
    {
        public override bool IsRoomAttributePossible(int enterDirection, int exitDirection)
        {
            return true;
        }
    }
}
