using GameCode.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameCode.GameAi
{
    public abstract class ALevelEnemiesPlacer : MonoBehaviour
    {
        public abstract LevelData PlaceEnemiesAsPerDifficulty(LevelData levelData, int numberOfRoomsToPlaceIn);

        protected bool IsRoomOnMainPath(int i, int j, List<IntPair> mainPath)
        {
            return mainPath.Any(coordinate => coordinate.x == i && coordinate.y == j);
        }
    }
}
