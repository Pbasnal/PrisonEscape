namespace Assets.Scripts.LayoutGenerator
{
    public interface ILayoutCreator
    {
        RoomBuilder[,] GenerateRoomLayout(Size startingLocation);
    }
}
