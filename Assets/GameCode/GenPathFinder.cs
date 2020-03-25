using GameCode.Models;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(AstarPath))]
public class GenPathFinder : MonoBehaviour
{
    private AstarPath _pathFinder;

    // Start is called before the first frame update
    void Start()
    {
        _pathFinder = GetComponent<AstarPath>();
    }

    public void GenerateAstar(LevelData levelData)
    {
        var graph = (GridGraph)AstarPath.active.data.AddGraph(typeof(GridGraph));

        var bounds = levelData.LevelBounds;
        var roomSize = levelData.RoomSize;

        var width = (int)(bounds.maxX - bounds.minX) + 1;
        var depth = (int)(bounds.maxY - bounds.minY) + 1;

        graph.center = new Vector3(
            transform.position.x + (width - roomSize.Width) / 2,
            transform.position.y - (depth - roomSize.Height) / 2, 0);

        _pathFinder.transform.position = new Vector3(
            transform.position.x + (width - roomSize.Width) / 2,
            transform.position.y - (depth - roomSize.Height) / 2, 0);

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
