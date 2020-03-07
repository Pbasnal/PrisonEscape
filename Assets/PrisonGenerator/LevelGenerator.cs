using Assets.Scripts.LayoutGenerator;
using Assets.Scripts.LevelRenderer;
using Pathfinding;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LevelAi))]
[RequireComponent(typeof(RoomCollection))]
[RequireComponent(typeof(LevelRenderer))]
public class LevelGenerator : MonoBehaviour
{
    public SizeObject levelSize;
    
    public GameObject astarPathObject;
    public GameObject enemy;
    public GameObject target;

    private RoomCollection roomCollection;

    private RoomBuilder _startingRoom;
    
    private Bounds bounds;
    private bool astarCalculated = false;

    private ILayoutCreator layoutCreator;
    private LevelRenderer levelRenderer;

    private void Start()
    {
        if (levelSize == null)
        {
            return;
        }

        var startingPoint = new LevelCoordinate
        {
            Height = 0,
            Width = UnityEngine.Random.Range(0, levelSize.Width)
        };

        levelRenderer = GetComponent<LevelRenderer>();
        roomCollection = GetComponent<RoomCollection>();

        layoutCreator = new FourByFourLayout(roomCollection.GetRoomTypes(), startingPoint);
        
        bounds = levelRenderer.RenderBaseLevel(layoutCreator);

        //StartCoroutine("GenerateAstarGraph");
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
}

public class Bounds
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
}
