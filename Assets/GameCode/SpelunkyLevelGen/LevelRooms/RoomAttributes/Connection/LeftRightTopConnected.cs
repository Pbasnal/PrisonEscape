using UnityEngine;

namespace LockdownGames.GameCode.SpelunkyLevelGen.LevelRooms.RoomAttributes
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
