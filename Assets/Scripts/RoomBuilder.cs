using UnityEngine;
using UnityEngine.Tilemaps;

public enum RoomType
{
    Blocked,
    LR,
    LRT,
    LRTB
}

[RequireComponent(typeof(Grid))]
[RequireComponent(typeof(TilemapRenderer))]
public class RoomBuilder : MonoBehaviour, ILevelRoom
{
    public SizeObject roomSize;
    public RoomType roomType;

    public bool IsRoomPossible(int enterDirection, int exitDirection)
    {
        return true;
    }
}