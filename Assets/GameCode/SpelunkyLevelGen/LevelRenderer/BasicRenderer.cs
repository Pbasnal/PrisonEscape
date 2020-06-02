using System;
using System.Collections.Generic;
using System.Linq;
using LockdownGames.GameCode.Models;
using LockdownGames.GameCode.SpelunkyLevelGen.LevelRooms;
using LockdownGames.GameCode.SpelunkyLevelGen.LevelRooms.RoomScripts;

using UnityEngine;


namespace LockdownGames.GameCode.SpelunkyLevelGen.LevelRenderer
{
    public class BasicRenderer : MonoBehaviour
    {
        [TextArea]
        public string info = "Renderer takes the final generated layout and renders all the rooms in it.";

        [Space(10)]
        [Header("Required")]
        public GameObject wall;

        [Space(10)]
        [Header("Debug - Set to get debug info")]
        public GameObject mainPathMarker;
        public GameObject startingRoomMarker;

        private RoomProvider roomProvider;

        public LevelData RenderBaseLevel(LevelData levelData)
        {
            var levelLayout = levelData.LevelLayout;
            var levelSize = levelData.LevelSize;

            roomProvider = levelData.RoomProvider;

            List<RoomBuilder> createdRooms = new List<RoomBuilder>();

            for (int i = 0; i < levelSize.x; i++)
            {
                for (int j = 0; j < levelSize.y; j++)
                {
                    var room = roomProvider.GetARoom(levelLayout.AttributeLayout[i, j]);
                    room = RenderRoom(room, i, j);
                    levelLayout.AddRenderedRoom(i, j, room.gameObject);
                }
            }


            RenderDebugInfoIfNeeded(levelData);

            levelData.SetBounds(GenerateWalls(levelData));
            return levelData;
        }

        private void RenderDebugInfoIfNeeded(LevelData levelData)
        {
            var levelLayout = levelData.LevelLayout;
            var levelSize = levelData.LevelSize;

            if (startingRoomMarker != null)
            {
                var room = levelLayout.Rooms[levelData.StartingRoomCoordinates.x, levelData.StartingRoomCoordinates.y];
                var marker = Instantiate(startingRoomMarker, room.transform.position, Quaternion.identity) as GameObject;
                marker.transform.localScale = new Vector3(levelData.RoomSize.x, levelData.RoomSize.y, 1);
            }

            if (mainPathMarker == null)
            {
                return;
            }

            for (int i = 0; i < levelSize.x; i++)
            {
                for (int j = 0; j < levelSize.y; j++)
                {
                    if (!levelLayout.MainPath.Any(p => p.x == i && p.y == j))
                    {
                        continue;
                    }

                    var room = levelLayout.Rooms[i, j];
                    var marker = Instantiate(mainPathMarker, room.transform.position, Quaternion.identity) as GameObject;
                    marker.transform.localScale = new Vector3(levelData.RoomSize.x, levelData.RoomSize.y, 1);
                }
            }
        }

        private RoomBuilder RenderRoom(RoomBuilder room, int i, int j)
        {
            var roomSizeX = room.roomSize.y;// - 1;
            var roomSizeY = room.roomSize.x;// - 1;

            Vector2 spawnPosition = new Vector2(
                transform.position.x + (j * roomSizeX),
                transform.position.y - (i * roomSizeY));

            room = Instantiate(room, spawnPosition, Quaternion.identity);
            room.transform.parent = transform;

            return room;
        }

        private LevelBounds GenerateWalls(LevelData levelData)
        {
            var scale = (levelData.LevelSize.x * levelData.RoomSize.x);

            var minX = transform.position.x - (roomProvider.RoomSize.y / 2) + 0.5f;
            var midX = (transform.position.x + scale) / 2 - (roomProvider.RoomSize.y / 2);
            var maxX = transform.position.x + scale - (roomProvider.RoomSize.y / 2) - 0.5f;

            var minY = transform.position.y - (roomProvider.RoomSize.x / 2) + 0.5f;
            var midY = (transform.position.y + scale) / 2 - (roomProvider.RoomSize.x / 2);
            var maxY = transform.position.y + scale - (roomProvider.RoomSize.x / 2) - 0.5f;

            var ground = Instantiate(wall, transform);
            var roof = Instantiate(wall, transform);
            var leftWall = Instantiate(wall, transform);
            var rightWall = Instantiate(wall, transform);

            ground.transform.position = new Vector3(midX, -minY, 0);
            ground.GetComponent<SpriteRenderer>().size = new Vector3(scale, 1, 1);
            ground.GetComponent<BoxCollider2D>().size = new Vector3(scale, 1, 1);

            roof.transform.position = new Vector3(midX, -maxY, 0);
            roof.GetComponent<SpriteRenderer>().size = new Vector3(scale, 1, 1);
            roof.GetComponent<BoxCollider2D>().size = new Vector3(scale, 1, 1);

            leftWall.transform.position = new Vector3(minX, -midY, 0);
            leftWall.GetComponent<SpriteRenderer>().size = new Vector3(1, scale, 1);
            leftWall.GetComponent<BoxCollider2D>().size = new Vector3(1, scale, 1);

            rightWall.transform.position = new Vector3(maxX, -midY, 0);
            rightWall.GetComponent<SpriteRenderer>().size = new Vector3(1, scale, 1);
            rightWall.GetComponent<BoxCollider2D>().size = new Vector3(1, scale, 1);

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
