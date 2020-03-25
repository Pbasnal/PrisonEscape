using GameCode.GameAi;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Grid))]
[RequireComponent(typeof(TilemapRenderer))]
public class RoomBuilder : MonoBehaviour
{
    public SizeObject roomSize;
    public ARoomType roomType;

    public Transform[] EnemySpawnPositions;
    public EnemyCollection EnemyCollection;

    public void InitializeRoomBuilder()
    {
        if (EnemySpawnPositions == null || EnemySpawnPositions.Length == 0)
        {
            return;
        }

        roomType.InitializeRoom(EnemySpawnPositions.Select(t => (Vector2)t.position).ToArray(),
                                EnemyCollection, 
                                transform);
    }
}