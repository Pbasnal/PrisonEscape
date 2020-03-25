namespace LayoutGenerator
{
    public interface ILayoutCreator
    {
        Size LevelSize { get; }
        LevelCoordinate StartingPoint { get; }
        LevelLayout GenerateRoomLayout();
    }
}
