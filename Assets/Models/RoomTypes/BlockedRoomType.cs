using UnityEngine;

[CreateAssetMenu(fileName = "Blocked Room", menuName = "Base Room Types/Blocked Room Type", order = 51)]
public class BlockedRoomType : IRoomType
{
    public override bool IsRoomPossible(int enterDirection, int exitDirection)
    {
        return false;
    }
}
