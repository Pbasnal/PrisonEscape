using UnityEngine;

namespace LockdownGames.GameCode.SpelunkyLevelGen.LevelRooms.RoomAttributes
{
    [CreateAssetMenu(fileName = "Prison Room", menuName = "RoomAttributes/Class/Prison", order = 51)]
    public class PrisonRoom : RoomClassAttribute
    {
        public override bool IsRoomAttributePossible(int enterDirection, int exitDirection)
        {
            return false;
        }
    }
}
