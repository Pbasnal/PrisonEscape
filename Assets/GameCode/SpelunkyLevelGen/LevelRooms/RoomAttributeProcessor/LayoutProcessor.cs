using System;
using System.Collections.Generic;
using System.Linq;

using LockdownGames.GameCode.Models;
using LockdownGames.GameCode.SpelunkyLevelGen.LevelRooms.RoomAttributes;

using UnityEngine;

namespace LockdownGames.GameCode.SpelunkyLevelGen.LevelRooms.RoomAttributeProcessor
{
    public abstract class LayoutProcessor : AttributeProcessor<RoomConnectionAttribute>
    {
        protected int[,][] directionsMap;

        private List<RoomConnectionAttribute> roomConnectionAttributes;
        
        public LevelLayout CreateLevelLayout(LevelData levelData)
        {
            return Process<LevelLayout>(levelData);
        }

        private void Initialize(RoomProvider roomProvider)
        {
            directionsMap = GetDirectionsMap();
            roomConnectionAttributes = roomProvider.GetUniqueAttributesOfType<RoomConnectionAttribute>();
        }

        protected override R Process<R>(LevelData levelData)
        {
            var levelLayout = new LevelLayout(LevelSize);

            if (levelLayout as R == null)
            {
                throw new Exception("Type must be LevelLayout");
            }

            Initialize(levelData.RoomProvider);
            levelData.SetLevelLayout(levelLayout);

            GenerateMainPath(levelData, levelLayout);

            FillRemainingLayout(levelData);

            return levelLayout as R;
        }

        private void FillRemainingLayout(LevelData levelData)
        {
            for (int i = 0; i < LevelSize.x; i++)
            {
                for (int j = 0; j < LevelSize.y; j++)
                {
                    if (levelData.LevelLayout.AttributeLayout[i, j].Any(a => a as RoomConnectionAttribute != null))
                    {
                        continue;
                    }

                    var connectionType = roomConnectionAttributes[UnityEngine.Random.Range(0, roomConnectionAttributes.Count)];

                    levelData.LevelLayout.AddRoomAttribute(i, j, connectionType);
                }
            }
        }

        private void GenerateMainPath(LevelData levelData, LevelLayout levelLayout)
        {
            int enterDirection = 0;
            var selectedPos = IntPair.CreatePair(levelData.StartingRoomCoordinates.x, levelData.StartingRoomCoordinates.y);

            while (selectedPos.x < LevelSize.x)
            {
                var selectedRoomConnection = SelectRoomConnectionType(enterDirection, selectedPos.y);

                if (selectedRoomConnection.SelectedRoomConnectionAttribute == null)
                {
                    throw new Exception("Couldn't select any room. Make sure your room inventory has sufficient combinations");
                }

                levelLayout.AddRoomAttribute(selectedPos.x, selectedPos.y, selectedRoomConnection.SelectedRoomConnectionAttribute);

                levelLayout.MainPath.Add(selectedPos.Clone());
                enterDirection = selectedRoomConnection.ExitDirection;

                switch (selectedRoomConnection.ExitDirection)
                {
                    case 1: selectedPos.y--; break;
                    case 2: selectedPos.y++; break;
                    case 3: selectedPos.x++; break;
                }
            }

            selectedPos.x--;

            levelData.EndRoomCoordinate = selectedPos.Clone();
        }

        private SelectedRoomTypeResponse SelectRoomConnectionType(int enterDirection, int width)
        {
            var selectedRoomIndex = 0;

            try
            {
                selectedRoomIndex = UnityEngine.Random.Range(0, directionsMap[enterDirection, width].Length);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                throw;
            }

            var exitDirection = directionsMap[enterDirection, width][selectedRoomIndex];
            var possibleRooms = new List<RoomConnectionAttribute>();
            foreach (var connectionAttribute in roomConnectionAttributes)
            {
                var isRoomPossible = connectionAttribute.IsRoomAttributePossible(enterDirection, exitDirection);
                if (!isRoomPossible)
                {
                    continue;
                }

                possibleRooms.Add(connectionAttribute);
            }

            return new SelectedRoomTypeResponse
            {
                SelectedRoomConnectionAttribute = possibleRooms.Count == 0 ? 
                    null : possibleRooms[UnityEngine.Random.Range(0, possibleRooms.Count)],
                ExitDirection = exitDirection
            };
        }

        protected abstract int[,][] GetDirectionsMap();
    }
}
