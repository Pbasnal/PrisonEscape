using System;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IEnemySpawner : IEventSystemHandler
{
    void SpawnEnemy(Transform target);
}

public class EnemySpawner : MonoBehaviour, IEnemySpawner
{
    public BasicSeekerAI enemy;
    public Transform[] spawnTransforms;

    private Guid roomId;

    private void Start()
    {
        roomId = Guid.NewGuid();
    }

    public void SpawnEnemy(Transform target)
    {
        Debug.Log("Message 1 received");

        while (AstarPath.active == null || AstarPath.active.graphs.Length == 0)
        {
            return;
        }

        var spawnPoint = spawnTransforms[UnityEngine.Random.Range(0, spawnTransforms.Length)];

        var enemyObject = Instantiate(enemy, spawnPoint.position, Quaternion.identity);
        var seekerAi = enemyObject.GetComponent<BasicSeekerAI>();
        seekerAi.target = target;

        AIBlackBoard.AddEnemyAndRoom(roomId, seekerAi);
    }
}
