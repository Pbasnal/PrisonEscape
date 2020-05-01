using UnityEngine;

namespace LockdownGames.GameCode.SpelunkyLevelGen.LevelRooms.RoomAttributes
{
    [CreateAssetMenu(fileName = "Hall Room", menuName = "RoomAttributes/Class/Hall", order = 51)]
    public class HallRoom : RoomClassAttribute
    {
        public override bool IsRoomAttributePossible(int enterDirection, int exitDirection)
        {
            return false;
        }
    }
}
