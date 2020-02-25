using UnityEngine;
using UnityEngine.Tilemaps;

public interface  IRoomType
{
    bool IsRoomPossible(int enterDirection, int exitDirection);
}

public class BlockedRoomType : IRoomType
{
    public bool IsRoomPossible(int enterDirection, int exitDirection)
    {
        return false;
    }
}

public class LeftRightRoomType : IRoomType
{
    public bool IsRoomPossible(int enterDirection, int exitDirection)
    {
        return true;
    }
}

public class LeftRightTopRoomType : IRoomType
{
    public bool IsRoomPossible(int enterDirection, int exitDirection)
    {
        return true;
    }
}

public class LeftRightTopBottomRoomType : IRoomType
{
    public bool IsRoomPossible(int enterDirection, int exitDirection)
    {
        return true;
    }
}

[RequireComponent(typeof(Grid))]
[RequireComponent(typeof(TilemapRenderer))]
public class RoomBuilder : MonoBehaviour//, ILevelRoom
{
    public SizeObject roomSize;
    public IRoomType roomType;

    //public bool IsRoomPossible(int enterDirection, int exitDirection)
    //{
    //    return true;
    //}
}