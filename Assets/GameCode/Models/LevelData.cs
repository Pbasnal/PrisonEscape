using LayoutGenerator;
using SpelunkyLevelGen.LevelGenerator.LevelRooms;
using SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomAttributes;
using System;
using UnityEngine;

namespace GameCode.Models
{
    public class LevelData
    {
        public LevelBounds LevelBounds { get; private set; }
        public ISize RoomSize { get; private set; }
        public ISize LevelSize { get; private set; }

        public LevelCoordinate StartingRoomCoordinates { get; private set; }
        public LevelLayout LevelLayout { get; private set; }


        public GameObject StartingRoom => LevelLayout.Rooms[StartingRoomCoordinates.Height, StartingRoomCoordinates.Width];
        public GameObject EndRoom => LevelLayout.Rooms[EndRoomCoordinate.Height, EndRoomCoordinate.Width];

        public RoomProvider RoomProvider
        {
            get { return _roomProvider; }
            set
            {
                _roomProvider = value ?? throw new Exception("Provided RoomProvider is null");
            }
        }

        public LevelCoordinate EndRoomCoordinate
        {
            get { return _endRoomCoordinate; }
            set
            {
                _endRoomCoordinate = value ?? throw new Exception("Provided End room coordinate is null");
            }
        }

        private GameObject _startinRoom;
        private RoomProvider _roomProvider;
        private LevelCoordinate _endRoomCoordinate;

        public void SetLevelSize(ISize levelSize)
        {
            LevelSize = levelSize ?? throw new Exception("Input LevelSize is empty");
        }

        public void SetBounds(LevelBounds bounds)
        {
            LevelBounds = bounds ?? throw new Exception("Input LevelBounds is empty");
        }

        public void SetStartingRoomCoordinates(LevelCoordinate coordinate)
        {
            StartingRoomCoordinates = coordinate ?? throw new Exception("Input StartingRoomCoordinate is empty");
        }

        public void SetRoomSize(SizeObject roomSize)
        {
            RoomSize = roomSize ?? throw new Exception("Input room size is empty");
        }

        public void SetLevelLayout(LevelLayout levelLayout)
        {
            LevelLayout = levelLayout ?? throw new Exception("Input level layout is empty");
        }
    }
}