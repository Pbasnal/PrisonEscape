using System.Collections.Generic;

namespace LayoutGenerator
{
    public class LevelLayout
    {
        public Size LevelSize;
        public LevelCoordinate StartingPostion;
        public ARoomType[,] TypeLayout;
        public List<LevelCoordinate> MainPath;

        public LevelLayout(Size levelSize, LevelCoordinate startingPostion)
        {
            LevelSize = levelSize;
            TypeLayout = new ARoomType[levelSize.Height, levelSize.Width];
            MainPath = new List<LevelCoordinate>();
            StartingPostion = startingPostion;
        }
    }
}


