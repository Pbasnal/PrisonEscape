using LayoutGenerator;
using System;

namespace GameCode.Models
{
    public class LevelData
    {
        public LevelBounds LevelBounds { get; private set; }
        public ISize RoomSize { get; private set; }
        public ISize LevelSize { get; private set; }

        public RoomCollection RoomCollection { get; private set; }

        public LevelCoordinate StartingRoomCoordinates { get; private set; }
        public LevelLayout LevelLayout { get; private set; }

        public StartingRoom StartingRoom { get; private set; }

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

        public void SetRoomCollection(RoomCollection roomCollection)
        {
            RoomCollection = roomCollection ?? throw new Exception("Input room colelction is empty");
        }

        public void SetLevelLayout(LevelLayout levelLayout)
        {
            LevelLayout = levelLayout ?? throw new Exception("Input level layout is empty");
        }

        public void SetStartingRoom(RoomBuilder startingRoom)
        {
            if (startingRoom == null || startingRoom.GetComponent<StartingRoom>() == null)
            {
                throw new Exception("Input starting room is empty");
            }
            StartingRoom = startingRoom.GetComponent<StartingRoom>();
        }
    }
}