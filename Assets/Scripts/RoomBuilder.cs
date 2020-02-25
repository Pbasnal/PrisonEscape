using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class IRoomType: ScriptableObject
{
    public abstract bool IsRoomPossible(int enterDirection, int exitDirection);
}

[CreateAssetMenu(fileName = "Blocked Room", menuName = "Base Room Types/Blocked Room Type", order = 51)]
public class BlockedRoomType : IRoomType
{
    public override bool IsRoomPossible(int enterDirection, int exitDirection)
    {
        return false;
    }
}

[CreateAssetMenu(fileName = "LR Room", menuName = "Base Room Types/LR Room Type", order = 51)]
public class LeftRightRoomType : IRoomType
{
    public override bool IsRoomPossible(int enterDirection, int exitDirection)
    {
        return true;
    }
}

[CreateAssetMenu(fileName = "LRT Room", menuName = "Base Room Types/LRT Room Type", order = 51)]
public class LeftRightTopRoomType : IRoomType
{
    public override bool IsRoomPossible(int enterDirection, int exitDirection)
    {
        return true;
    }
}

[CreateAssetMenu(fileName = "LRTB Room", menuName = "Base Room Types/LRTB Room Type", order = 51)]
public class LeftRightTopBottomRoomType : IRoomType
{
    public override bool IsRoomPossible(int enterDirection, int exitDirection)
    {
        return true;
    }
}

[RequireComponent(typeof(Grid))]
[RequireComponent(typeof(TilemapRenderer))]
public class RoomBuilder : MonoBehaviour
{
    public SizeObject roomSize;
    public IRoomType roomType;
}