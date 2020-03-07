using UnityEngine;

public abstract class IRoomType: ScriptableObject
{
    public abstract bool IsRoomPossible(int enterDirection, int exitDirection);
}
