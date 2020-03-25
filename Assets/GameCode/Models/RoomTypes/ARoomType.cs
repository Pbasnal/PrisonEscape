using GameCode;
using GameCode.GameAi;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ARoomType : MonoBehaviour//ScriptableObject
{
    //public abstract RoomType;
    public Vector2[] enemySpawnPosition;
    protected EnemyCollection enemyCollection;
    protected Transform roomTransform;

    public string id;

    public abstract bool IsRoomPossible(int enterDirection, int exitDirection);

    public virtual void InitializeRoom(Vector2[] positions, EnemyCollection enemyCollection, Transform roomTransform)
    {
        id = Guid.NewGuid().ToString();
        Debug.Log("Setting enemies " + id);
        this.enemyCollection = enemyCollection;
        enemySpawnPosition = positions.Select(p => new Vector2(p.x, p.y)).ToArray();
        this.roomTransform = roomTransform;
    }

    public virtual void SpawnEnemies(int numberOfEnemies)
    {
        var selectedPositions = new List<int>();

        if (enemySpawnPosition.Length == 0)
        {
            Debug.Log("Enemies not set " + id);
            return; // this is usually the starting room
        }

        for (int i = 0; i < numberOfEnemies; i++)
        {
            var spawnPositionIndex = Utilities.RandomRangeWithoutRepeat(0, enemySpawnPosition.Length, selectedPositions);
            if (spawnPositionIndex == -1)
            {
                continue;
            }
            Instantiate(enemyCollection.GetAnEnemy(), enemySpawnPosition[spawnPositionIndex], Quaternion.identity);

            selectedPositions.Add(spawnPositionIndex);
        }
    }
}