using GameCode.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameCode.GameAi
{
    public abstract class ALevelEnemiesPlacer : MonoBehaviour
    {
        public abstract LevelData PlaceEnemiesAsPerDifficulty(LevelData levelData, int numberOfRoomsToPlaceIn);

        protected bool IsRoomOnMainPath(int i, int j, List<LevelCoordinate> mainPath)
        {
            return mainPath.Any(coordinate => coordinate.Height == i && coordinate.Width == j);
        }
    }
}
