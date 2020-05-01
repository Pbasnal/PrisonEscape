namespace LockdownGames.GameCode.Interfaces
{
    public interface ILevelRoom
    {
        bool IsRoomPossible(int enterDirection, int exitDirection);
    }
}