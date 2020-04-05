using GameCode.Models;
using LayoutGenerator;
using SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomAttributeProcessor
{
    public abstract class LayoutProcessor : AttributeProcessor<RoomConnectednessType>
    {
        protected int[,][] directionsMap;

        private List<RoomAttribute<RoomConnectednessType>> roomConnectionAttributes;
        
        public LevelLayout CreateLevelLayout(LevelData levelData)
        {
            return Process<LevelLayout>(levelData);
        }

        private void Initialize(RoomProvider roomProvider)
        {
            directionsMap = GetDirectionsMap();
            roomConnectionAttributes = roomProvider.GetUniqueAttributesOfType<RoomAttribute<RoomConnectednessType>>();
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
            for (int i = 0; i < LevelSize.Height; i++)
            {
                for (int j = 0; j < LevelSize.Width; j++)
                {
                    if (levelData.LevelLayout.AttributeLayout[i, j].Any(a => a as RoomAttribute<RoomConnectednessType> != null))
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
            var selectedPos = new LevelCoordinate
            {
                Height = levelData.StartingRoomCoordinates.Height,
                Width = levelData.StartingRoomCoordinates.Width
            };

            while (selectedPos.Height < LevelSize.Height)
            {
                var selectedRoomConnection = SelectRoomConnectionType(enterDirection, selectedPos.Width);

                if (selectedRoomConnection.SelectedRoomAttributeSO == null)
                {
                    throw new Exception("Couldn't select any room. Make sure your room inventory has sufficient combinations");
                }

                levelLayout.AddRoomAttribute(selectedPos.Height, selectedPos.Width, selectedRoomConnection.SelectedRoomAttributeSO);

                levelLayout.MainPath.Add(selectedPos.Clone());
                enterDirection = selectedRoomConnection.ExitDirection;

                switch (selectedRoomConnection.ExitDirection)
                {
                    case 1: selectedPos.Width--; break;
                    case 2: selectedPos.Width++; break;
                    case 3: selectedPos.Height++; break;
                }
            }

            selectedPos.Height--;

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
            var possibleRooms = new List<RoomAttribute<RoomConnectednessType>>();
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
                SelectedRoomAttributeSO = possibleRooms.Count == 0 ? null : possibleRooms[UnityEngine.Random.Range(0, possibleRooms.Count)],
                ExitDirection = exitDirection
            };
        }

        protected abstract int[,][] GetDirectionsMap();
    }
}
