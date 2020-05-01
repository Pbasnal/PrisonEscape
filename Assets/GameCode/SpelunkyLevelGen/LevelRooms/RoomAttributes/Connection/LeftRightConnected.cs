using UnityEngine;

namespace LockdownGames.GameCode.SpelunkyLevelGen.LevelRooms.RoomAttributes
{
    [CreateAssetMenu(fileName = "LR Room", menuName = "RoomAttributes/Connectedness/LR", order = 51)]
    public class LeftRightConnected : RoomConnectionAttribute
    {
        public override bool IsRoomAttributePossible(int enterDirection, int exitDirection)
        {
            return false;
        }
    }
}
