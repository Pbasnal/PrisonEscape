﻿using System.Collections.Generic;

using LockdownGames.GameCode.Models;
using LockdownGames.GameCode.SpelunkyLevelGen.LevelRooms.RoomScripts;
using LockdownGames.Utilities;

namespace LockdownGames.GameAi.LevelAi
{
    public class RandomEnemyPlacer : ALevelEnemiesPlacer
    {
        public EnemyCollection EnemyCollection;

        private List<IntPair> selectedRooms;

        private List<int> selectedIndexes;

        public override LevelData PlaceEnemiesAsPerDifficulty(LevelData levelData, int numberOfRoomsToPlaceIn)
        {
            selectedRooms = new List<IntPair>();
            selectedIndexes = new List<int>();

            for (int i = 0; i < numberOfRoomsToPlaceIn; i++)
            {
                var selectedCoordinate = GetANewRandomRoom(levelData); // skip for starting room
                if (selectedCoordinate == null || IsStartingRoom(selectedCoordinate, levelData))
                {
                    continue;
                }

                var enemySpawner = levelData.LevelLayout.Rooms[selectedCoordinate.x, selectedCoordinate.y].GetComponent<ObjectSpawner>();

                if (enemySpawner == null)
                {
                    continue;
                }

                enemySpawner.SpawnObject(EnemyCollection.GetAnEnemy().gameObject, enemySpawner.TotalSpawnPoints);
            }

            return levelData;
        }

        private bool IsStartingRoom(IntPair selectedCoordinate, LevelData levelData)
        {
            return levelData.StartingRoomCoordinates.x == selectedCoordinate.x
                && levelData.StartingRoomCoordinates.y == selectedCoordinate.y;
        }

        private IntPair GetANewRandomRoom(LevelData levelData)
        {
            var levelHeight = levelData.LevelLayout.AttributeLayout.GetLength(0);
            var levelWidth = levelData.LevelLayout.AttributeLayout.GetLength(1);

            var selectedIndex = Helper.RandomRangeWithoutRepeat(0, levelHeight * levelWidth, selectedIndexes);

            if (selectedIndex == -1)
            {
                return null;
            }

            selectedIndexes.Add(selectedIndex);

            var selectedWidth = selectedIndex / levelWidth;
            var selectedHeight = selectedIndex % levelWidth;

            return IntPair.CreatePair(selectedHeight, selectedWidth);
        }
    }
}
