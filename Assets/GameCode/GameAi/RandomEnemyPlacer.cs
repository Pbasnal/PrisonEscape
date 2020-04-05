using GameCode.Models;
using SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomScripts;
using System.Collections.Generic;

namespace GameCode.GameAi
{
    public class RandomEnemyPlacer : ALevelEnemiesPlacer
    {
        public EnemyCollection EnemyCollection;

        private List<LevelCoordinate> selectedRooms;

        private List<int> selectedIndexes;

        public override LevelData PlaceEnemiesAsPerDifficulty(LevelData levelData, int numberOfRoomsToPlaceIn)
        {
            selectedRooms = new List<LevelCoordinate>();
            selectedIndexes = new List<int>();

            for (int i = 0; i < numberOfRoomsToPlaceIn; i++)
            {
                var selectedCoordinate = GetANewRandomRoom(levelData); // skip for starting room
                if (selectedCoordinate == null || IsStartingRoom(selectedCoordinate, levelData))
                {
                    continue;
                }

                var enemySpawner = levelData.LevelLayout.Rooms[selectedCoordinate.Height, selectedCoordinate.Width].GetComponent<ObjectSpawner>();

                if (enemySpawner == null)
                {
                    continue;
                }

                enemySpawner.SpawnObject(EnemyCollection.GetAnEnemy().gameObject, enemySpawner.TotalSpawnPoints);
            }

            return levelData;
        }

        private bool IsStartingRoom(LevelCoordinate selectedCoordinate, LevelData levelData)
        {
            return levelData.StartingRoomCoordinates.Height == selectedCoordinate.Height
                && levelData.StartingRoomCoordinates.Width == selectedCoordinate.Width;
        }

        private LevelCoordinate GetANewRandomRoom(LevelData levelData)
        {
            var levelHeight = levelData.LevelLayout.AttributeLayout.GetLength(0);
            var levelWidth = levelData.LevelLayout.AttributeLayout.GetLength(1);

            var selectedIndex = Utilities.RandomRangeWithoutRepeat(0, levelHeight * levelWidth, selectedIndexes);

            if (selectedIndex == -1)
            {
                return null;
            }

            selectedIndexes.Add(selectedIndex);

            var selectedWidth = selectedIndex / levelWidth;
            var selectedHeight = selectedIndex % levelWidth;

            return new LevelCoordinate(selectedHeight, selectedWidth);

            //var selectedHeight = 0;
            //var selectedWidth = 0;
            //var roomGotSelected = false;



            //for (int i = 0; i < levelHeight * levelWidth - 1; i++)
            //{
            //    selectedHeight = Random.Range(0, levelHeight);
            //    selectedWidth = Random.Range(0, levelWidth);

            //    if (!selectedRooms.Any(c => c.Height == selectedHeight && c.Width == selectedWidth))
            //    {
            //        roomGotSelected = true;
            //        break;
            //    }
            //}

            //if (!roomGotSelected)
            //{
            //    return null;
            //}

            //var selectedCoordinate = new LevelCoordinate(selectedHeight, selectedWidth);
            //selectedRooms.Add(selectedCoordinate);

            //return selectedCoordinate;
        }
    }
}
