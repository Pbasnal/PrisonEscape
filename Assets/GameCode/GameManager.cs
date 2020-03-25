using Cinemachine;
using GameCode.GameAi;
using GameCode.Models;
using System.Collections;
using UnityEngine;

namespace Assets.GameCode
{
    public class GameManager : MonoBehaviour
    {
        public SizeObject LevelSize;
        public LevelGenerator LevelGenerator;
        public ALevelEnemiesPlacer LevelEnemiesPlacer;
        public GenPathFinder GenPathFinder;
        public GameObject player;

        public CinemachineVirtualCamera CVcam;

        private LevelData _levelData;

        private void Start()
        {
            StartCoroutine("Generatelevel");
        }

        private IEnumerator Generatelevel()
        {
            _levelData = GenerateAndRenderLevel();
            
            yield return new WaitForEndOfFrame();

            GenPathFinder.GenerateAstar(_levelData);
            //var enemyObject = Instantiate(enemy, _startingRoom.transform.position, Quaternion.identity);
            //var enemyScript = enemyObject.GetComponent<BasicSeekerAI>();
            //enemyScript.target = target.transform;
        }

        private LevelData GenerateAndRenderLevel()
        {
            _levelData = new LevelData();
            _levelData.SetLevelSize(LevelSize);
            _levelData = LevelGenerator.GenerateLevel(_levelData);

            var totalRooms = _levelData.LevelSize.Height * _levelData.LevelSize.Width;
            LevelEnemiesPlacer.PlaceEnemiesAsPerDifficulty(_levelData, totalRooms - 1);


            player = _levelData.StartingRoom.SpawnPlayer(player);

            CVcam.Follow = player.transform;

            return _levelData;
        }

    }
}
