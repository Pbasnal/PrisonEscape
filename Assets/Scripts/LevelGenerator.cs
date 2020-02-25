using Assets.Scripts.LayoutGenerator;
using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelAi))]
public class LevelGenerator : MonoBehaviour
{
    public RoomBuilder[] rooms;
    public SizeObject levelSize;
    public GameObject mainPathMarker;
    public GameObject startingRoomMarker;
    public GameObject wall;
    public GameObject astarPathObject;
    public GameObject enemy;
    public GameObject target;

    public RoomCollection roomCollection;

    private List<RoomBuilder> _possibleRooms = new List<RoomBuilder>();
    private RoomBuilder[,] _levelLayout;
    private RoomBuilder _startingRoom;
    private Size _startingRoomIndex;

    private Bounds bounds;
    private bool astarCalculated = false;

    private ILayoutCreator layoutCreator;

    private void Awake()
    {
        if (levelSize == null)
        {
            return;
        }

        layoutCreator = new FourByFourLayout(roomCollection.GetRooms());

        _startingRoomIndex = new Size
        {
            Height = levelSize.Height - 1,  // this was set to zero and to fix it, it needs the understanding that the level is getting generated from height to 0
            Width = UnityEngine.Random.Range(0, levelSize.Width)
        };

        var roomTypeLayout = layoutCreator.GenerateRoomLayout(_startingRoomIndex);
        _startingRoom = _levelLayout[_startingRoomIndex.Height, _startingRoomIndex.Width];

        RenderLevelLayout();
        bounds = GenerateWalls();

        StartCoroutine("GenerateAstarGraph");
    }

    private IEnumerator GenerateAstarGraph()
    {
        yield return new WaitForEndOfFrame();

        GenerateAstar(bounds);
        astarCalculated = true;
        var enemyObject = Instantiate(enemy, _startingRoom.transform.position, Quaternion.identity);
        var enemyScript = enemyObject.GetComponent<BasicSeekerAI>();
        enemyScript.target = target.transform;
    }

    private void Update()
    {

    }

    private void GenerateAstar(Bounds bounds)
    {
        var graph = (GridGraph)AstarPath.active.data.AddGraph(typeof(GridGraph));

        var width = (int)(bounds.maxX - bounds.minX) + 1;
        var depth = (int)(bounds.maxY - bounds.minY) + 1;

        graph.center = new Vector3(
            transform.position.x + (width - _startingRoom.roomSize.Width) / 2,
            transform.position.y - (depth - _startingRoom.roomSize.Height) / 2, 0);

        astarPathObject.transform.position = new Vector3(
            transform.position.x + (width - _startingRoom.roomSize.Width) / 2,
            transform.position.y - (depth - _startingRoom.roomSize.Height) / 2, 0);

        graph.rotation = new Vector3(-90, 270, 90);

        graph.neighbours = NumNeighbours.Four;
        graph.SetDimensions(width, depth, 1);
        graph.collision.mask = LayerMask.GetMask("Obstacles");
        //graph.collision.type = ColliderType.Ray;
        graph.collision.diameter = 0.2f;
        graph.collision.use2D = true;
        AstarPath.active.Scan();
    }
    
    private RoomBuilder[,] GenerateRoomLayout(Size startingLocation)
    {
        _levelLayout = new RoomBuilder[levelSize.Height, levelSize.Width];

        int enterDirection = 0;
        var currentLocation = new Size
        {
            Height = startingLocation.Height,
            Width = startingLocation.Width
        };

        var directions = GetDirectionsMap();
        var generationStartTime = DateTime.UtcNow;
        var retries = 0;
        while (currentLocation.Height >= 0 && retries < 5)
        {
            int i = enterDirection;
            int j = currentLocation.Width;
            int exitDirection = 0;
            int selectedRoom = -1;
            try
            {
                selectedRoom = UnityEngine.Random.Range(0, directions[i, j].Length);
                exitDirection = directions[i, j][selectedRoom];
                _possibleRooms = GetPossibleRooms(enterDirection, exitDirection);

                if (_possibleRooms.Count == 0)
                {
                    retries++;
                    //Debug.LogWarning(String.Format("i: {0}  j: {1}  In: {2}  Out: {3}", i, j, enterDirection, exitDirection));
                    continue;
                }
                retries = 0;

                var possibleRoom = _possibleRooms[UnityEngine.Random.Range(0, _possibleRooms.Count)];
                _levelLayout[currentLocation.Height, currentLocation.Width] = possibleRoom;
            }
            catch (Exception ex)
            {
                Debug.Log(string.Format("i: {0}   j: {1}  selected: {2}", i, j, selectedRoom));
                Debug.LogException(ex);
            }
            //Debug.Log(String.Format("i: {0}  j: {1}  SelectedRoom: {2}  In: {3}  Out: {4}", i, j, selectedRoom, enterDirection, exitDirection));

            switch (exitDirection)
            {
                case 1: currentLocation.Width--; break;
                case 2: currentLocation.Width++; break;
                case 3: currentLocation.Height--; break;
            }

            enterDirection = exitDirection;
        }

        //Debug.Log("Time to generate the level: " + (DateTime.UtcNow - generationStartTime).TotalMilliseconds);
        return _levelLayout;
    }

    private void RenderLevelLayout()
    {
        var x = transform.position.x;
        var y = transform.position.y;

        for (int i = 0; i < _levelLayout.GetLength(0); i++)
        {
            for (int j = 0; j < _levelLayout.GetLength(1); j++)
            {
                GameObject marker = null;
                if (_levelLayout[i, j] == null)
                {
                    _levelLayout[i, j] = AssignRoomFor(i, j);
                    RenderRoom(_levelLayout[i, j], i, j);
                }
                else
                {
                    var room = RenderRoom(_levelLayout[i, j], i, j);
                    if (_startingRoomIndex.Height == i && _startingRoomIndex.Width == j)
                    {
                        marker = Instantiate(startingRoomMarker, room.transform.position, Quaternion.identity);
                    }
                    else
                    {
                        marker = Instantiate(mainPathMarker, room.transform.position, Quaternion.identity);
                    }
                    marker.transform.localScale = new Vector3(_levelLayout[i, j].roomSize.Width, _levelLayout[i, j].roomSize.Height, 1);
                    marker.transform.parent = room.transform;
                }
            }
        }
    }

    private RoomBuilder RenderRoom(RoomBuilder roomBuilder, int i, int j)
    {
        var roomSizeX = roomBuilder.roomSize.Width;
        var roomSizeY = roomBuilder.roomSize.Height;

        Vector2 spawnPosition = new Vector2(
            transform.position.x + (j * roomSizeX),
            transform.position.y - (i * roomSizeY));

        var room = Instantiate(_levelLayout[i, j], spawnPosition, Quaternion.identity);
        room.transform.parent = transform;

        return room;
    }

    private RoomBuilder AssignRoomFor(int indexI, int indexJ)
    {
        return rooms[UnityEngine.Random.Range(0, rooms.Length)];
    }

    private Bounds GenerateWalls()
    {
        var scale = levelSize.Height * _startingRoom.roomSize.Height;

        var minX = transform.position.x - (_startingRoom.roomSize.Width / 2) + 0.5f;
        var midX = (transform.position.x + scale) / 2 - (_startingRoom.roomSize.Width / 2);
        var maxX = transform.position.x + scale - (_startingRoom.roomSize.Width / 2) - 0.5f;

        var minY = transform.position.y - (_startingRoom.roomSize.Height / 2) + 0.5f;
        var midY = (transform.position.y + scale) / 2 - (_startingRoom.roomSize.Height / 2);
        var maxY = transform.position.y + scale - (_startingRoom.roomSize.Height / 2) - 0.5f;

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

        return new Bounds
        {
            minX = minX,
            maxX = maxX,
            minY = minY,
            maxY = maxY
        };
    }
}

public class Bounds
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
}
