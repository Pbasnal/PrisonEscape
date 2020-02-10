using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestMessagePublisher : MonoBehaviour
{
    public Transform player;
    private EnemySpawner[] enemySpawners;

    private void Start()
    {
        StartCoroutine("DispatchMessages");
    }

    private IEnumerator DispatchMessages()
    {
        while (AstarPath.active == null || AstarPath.active.graphs.Length == 0)
        {
            yield return new WaitForSecondsRealtime(1);
        }

        enemySpawners = FindObjectsOfType<EnemySpawner>();

        foreach (var spawner in enemySpawners)
        {
            ExecuteEvents.Execute<IEnemySpawner>(spawner.gameObject, null, (x, y) => x.SpawnEnemy(player));
        }
    }
}
