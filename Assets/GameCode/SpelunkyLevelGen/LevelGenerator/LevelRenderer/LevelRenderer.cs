using GameCode.Models;
using LayoutGenerator;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.LevelRenderer
{
    [RequireComponent(typeof(RoomCollection))]
    public class LevelRenderer : MonoBehaviour
    {
        public GameObject mainPathMarker;
        public GameObject startingRoomMarker;
        public GameObject wall;

        private RoomCollection roomCollection;

        private void Awake()
        {
            roomCollection = GetComponent<RoomCollection>();
        }

        public LevelData RenderBaseLevel(LevelData levelData, StartingRoom startingRoom)
        {
            var roomTypeLayout = levelData.LevelLayout;

            var x = transform.position.x;
            var y = transform.position.y;

            var levelSize = levelData.LevelSize;

            List<RoomBuilder> createdRooms = new List<RoomBuilder>();

            for (int i = 0; i < levelSize.Height; i++)
            {
                for (int j = 0; j < levelSize.Width; j++)
                {
                    GameObject marker = null;
                    RoomBuilder room;
                    if (roomTypeLayout.StartingPostion.Height == i && roomTypeLayout.StartingPostion.Width == j)
                    {
                        room = RenderRoom(startingRoom, i, j);
                        levelData.SetStartingRoom(room);

                        marker = Instantiate(startingRoomMarker, room.transform.position, Quaternion.identity);
                        marker.transform.localScale = new Vector3(room.roomSize.Width, room.roomSize.Height, 1);
                        marker.transform.parent = room.transform;
                    }
                    else
                    {
                        room = RenderRoom(roomTypeLayout.TypeLayout[i, j], i, j);
                    }

                    roomTypeLayout.TypeLayout[i, j] = room.roomType;
                    createdRooms.Add(room);

                    if (roomTypeLayout.MainPath.Any(p => p.Height == i && p.Width == j))
                    {
                        marker = Instantiate(mainPathMarker, room.transform.position, Quaternion.identity);
                        marker.transform.localScale = new Vector3(room.roomSize.Width, room.roomSize.Height, 1);
                        marker.transform.parent = room.transform;
                    }
                }
            }

            levelData.SetBounds(GenerateWalls(levelData));
            return levelData;
        }

        private RoomBuilder RenderRoom(ARoomType roomType, int i, int j)
        {
            var room = roomCollection.GetARoom(roomType);
            return RenderRoom(room, i, j);
        }

        private RoomBuilder RenderRoom(RoomBuilder room, int i, int j)
        {
            var roomSizeX = room.roomSize.Width;
            var roomSizeY = room.roomSize.Height;

            Vector2 spawnPosition = new Vector2(
                transform.position.x + (j * roomSizeX),
                transform.position.y - (i * roomSizeY));

            room = Instantiate(room, spawnPosition, Quaternion.identity);
            room.transform.parent = transform;
            room.InitializeRoomBuilder();

            return room;
        }

        private LevelBounds GenerateWalls(LevelData levelData)
        {
            var scale = levelData.LevelSize.Height * levelData.RoomSize.Height;

            var minX = transform.position.x - (roomCollection.RoomSize.Width / 2) + 0.5f;
            var midX = (transform.position.x + scale) / 2 - (roomCollection.RoomSize.Width / 2);
            var maxX = transform.position.x + scale - (roomCollection.RoomSize.Width / 2) - 0.5f;

            var minY = transform.position.y - (roomCollection.RoomSize.Height / 2) + 0.5f;
            var midY = (transform.position.y + scale) / 2 - (roomCollection.RoomSize.Height / 2);
            var maxY = transform.position.y + scale - (roomCollection.RoomSize.Height / 2) - 0.5f;

            var ground = Instantiate(wall, transform);
            var roof = Instantiate(wall, transform);
            var leftWall = Instantiate(wall, transform);
            var rightWall = Instantiate(wall, transform);

            ground.transform.position = new Vector3(midX, -minY, 0);
            ground.transform.localScale = new Vector3(scale, 1, 1);

            roof.transform.position = new Vector3(midX, -maxY, 0);
            roof.transform.localScale = new Vector3(scale, 1, 1);

            leftWall.transform.position = new Vector3(minX, -midY, 0);
            leftWall.transform.localScale = new Vector3(1, scale, 1);

            rightWall.transform.position = new Vector3(maxX, -midY, 0);
            rightWall.transform.localScale = new Vector3(1, scale, 1);

            return new LevelBounds
            {
                minX = minX,
                maxX = maxX,
                minY = minY,
                maxY = maxY
            };
        }
    }
}
