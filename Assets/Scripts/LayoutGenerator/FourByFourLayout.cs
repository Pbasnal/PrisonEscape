using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.LayoutGenerator
{
    public class FourByFourLayout : ILayoutCreator
    {
        private Size levelSize = new Size { Height = 4, Width = 4 };
        private RoomBuilder[] _rooms;

        public FourByFourLayout(RoomBuilder[] rooms)
        {
            _rooms = rooms;
        }

        public RoomBuilder[,] GenerateRoomLayout(Size startingLocation)
        {
            var levelLayout = new RoomBuilder[levelSize.Height, levelSize.Width];

            int enterDirection = 0;
            var currentLocation = new Size
            {
                Height = startingLocation.Height,
                Width = startingLocation.Width
            };

            var generationStartTime = DateTime.UtcNow;
            while (currentLocation.Height >= 0)
            {
                PossibleRoomSelection possibleRoomSelection = null;
                try
                {
                    possibleRoomSelection = SelectARoom(enterDirection, currentLocation.Width);
                    
                    levelLayout[currentLocation.Height, currentLocation.Width] = possibleRoomSelection.SelectedRoom;
                }
                catch (Exception ex)
                {
                    Debug.Log(string.Format("i: {0}   j: {1}  selected: {2}", enterDirection, currentLocation.Width, possibleRoomSelection.SelectedRoom));
                    Debug.LogException(ex);
                }
                //Debug.Log(String.Format("i: {0}  j: {1}  SelectedRoom: {2}  In: {3}  Out: {4}", i, j, selectedRoom, enterDirection, exitDirection));

                switch (possibleRoomSelection.ExitDirection)
                {
                    case 1: currentLocation.Width--; break;
                    case 2: currentLocation.Width++; break;
                    case 3: currentLocation.Height--; break;
                }

                enterDirection = possibleRoomSelection.ExitDirection;
            }

            //Debug.Log("Time to generate the level: " + (DateTime.UtcNow - generationStartTime).TotalMilliseconds);s
            return levelLayout;
        }

        private int[,][] GetDirectionsMap()
        {
            /* 1 - Left
             * 2 - Right
             * 3 - Up
             */
            var directions = new int[4, 4][];

            directions[0, 0] = directions[3, 0] = new int[] { 2, 2, 3 };
            directions[0, 1] = directions[3, 1] = directions[0, 2] = directions[3, 2] = new int[] { 1, 1, 2, 2, 3 };
            directions[0, 3] = directions[3, 3] = new int[] { 1, 1, 3 };

            directions[1, 0] = new int[] { 3 };
            directions[1, 1] = directions[1, 2] = directions[0, 3];
            directions[1, 3] = new int[] { }; // this is not a possible situation

            directions[2, 0] = directions[1, 3]; // this is not a possible situation
            directions[2, 1] = directions[2, 2] = directions[0, 0];
            directions[2, 3] = directions[1, 0];

            return directions;
        }

        private PossibleRoomSelection SelectARoom(int enterDirection, int width)
        {
            var directions = GetDirectionsMap();
            var selectedRoomIndex = UnityEngine.Random.Range(0, directions[enterDirection, width].Length);
            var exitDirection = directions[enterDirection, width][selectedRoomIndex];
            var possibleRooms = new List<RoomBuilder>();
            foreach (var room in _rooms)
            {
                var isRoomPossible = room.roomType.IsRoomPossible(enterDirection, exitDirection);
                if (!isRoomPossible)
                {
                    continue;
                }

                possibleRooms.Add(room);
            }

            return new PossibleRoomSelection
            {
                EnterDirection = enterDirection,
                ExitDirection = exitDirection,
                SelectedRoom = possibleRooms[UnityEngine.Random.Range(0, possibleRooms.Count)],
                SelectedRoomIndex = selectedRoomIndex
            };
        }
    }

    public class PossibleRoomSelection
    {
        public RoomBuilder SelectedRoom;
        public int EnterDirection;
        public int ExitDirection;
        public int SelectedRoomIndex;
    }
}


