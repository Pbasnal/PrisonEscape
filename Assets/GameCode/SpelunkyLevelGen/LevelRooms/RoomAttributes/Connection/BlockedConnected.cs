using UnityEngine;

namespace SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomAttributes
{

    [CreateAssetMenu(fileName = "Blocked Room", menuName = "RoomAttributes/Connectedness/Blocked", order = 51)]
    public class BlockedConnected : RoomConnectionAttribute
    {
        public override bool IsRoomAttributePossible(int enterDirection, int exitDirection)
        {
            return false;
        }
    }
}
