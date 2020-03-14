using SpelunkyLevelGen.Models.Level;
using System.Collections;
using UnityEngine;

namespace Assets.GameCode
{
    public class GameManager : MonoBehaviour
    {
        public SizeObject LevelSize;
        public LevelGenerator LevelGenerator;
        public GenPathFinder GenPathFinder;

        private LevelData _levelData;

        private void Start()
        {
            StartCoroutine("Generatelevel");
        }

        private IEnumerator Generatelevel()
        {
            _levelData = GenerateAndRenderLevel();
            
            yield return new WaitForEndOfFrame();

            GenPathFinder.GenerateAstar(_levelData.LevelBounds, _levelData.RoomSize);
            //var enemyObject = Instantiate(enemy, _startingRoom.transform.position, Quaternion.identity);
            //var enemyScript = enemyObject.GetComponent<BasicSeekerAI>();
            //enemyScript.target = target.transform;
        }

        private LevelData GenerateAndRenderLevel()
        {
            _levelData = new LevelData();
            _levelData.SetLevelSize(LevelSize);
            _levelData = LevelGenerator.GenerateLevel(_levelData);

            return _levelData;
        }

    }
}
