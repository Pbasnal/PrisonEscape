using Cinemachine;
using GameAi.Code.Player;
using GameCode.GameAi;
using GameCode.Models;
using SpelunkyLevelGen.LevelGenerator.LevelObjects.ExitDoor;
using SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomScripts;
using System;
using System.Collections;
using UnityEngine;

namespace GameCode
{
    public enum GameState
    {
        NotStated,
        Running,
        Won,
        Lost
    }

    public class GameManager : MonoBehaviour
    {
        public SizeObject LevelSize;
        public LevelGenerator LevelGenerator;
        public ALevelEnemiesPlacer LevelEnemiesPlacer;
        public GenPathFinder GenPathFinder;

        public GameObject player;
        public ExitDoor ExitDoor;

        public CinemachineVirtualCamera CVcam;

        private LevelData _levelData;

        public static event Action<GameState> OnGameStateChange;

        private void Start()
        {
            StartCoroutine("Generatelevel");
        }

        private IEnumerator Generatelevel()
        {
            _levelData = GenerateAndRenderLevel();
            
            yield return new WaitForEndOfFrame();

            GenPathFinder.GenerateAstar(_levelData);

            yield return new WaitForEndOfFrame();

            var startingRoom = _levelData.StartingRoom.GetComponent<ObjectSpawner>();
            player = startingRoom.SpawnObject(player);
            Player.OnPlayerDeath += PlayerDied;
            CVcam.Follow = player.transform;

            ExitDoor = InitializeEndRoom(_levelData);
            ExitDoor.OnPlayerReachedEnd += PlayerWon;

            var totalRooms = _levelData.LevelSize.Height * _levelData.LevelSize.Width;
            LevelEnemiesPlacer.PlaceEnemiesAsPerDifficulty(_levelData, totalRooms - 1);
        }

        private LevelData GenerateAndRenderLevel()
        {
            _levelData = new LevelData();
            _levelData = LevelGenerator.GenerateLevel(_levelData);

            
            return _levelData;
        }

        private void PlayerDied()
        {
            Debug.Log("game over - player deid");
            OnGameStateChange?.Invoke(GameState.Lost);
        }

        private void PlayerWon()
        {
            Debug.Log("game over - player won");
            OnGameStateChange?.Invoke(GameState.Won);
        }

        private ExitDoor InitializeEndRoom(LevelData levelData)
        {
            var endRoom = levelData.EndRoom.GetComponent<RoomBuilder>();

            var roomSize = endRoom.roomSize;

            Vector2 doorPosition = new Vector2(endRoom.transform.position.x, endRoom.transform.position.y - (endRoom.roomSize.height / 2) + 0.5f);
                
            return Instantiate(ExitDoor, doorPosition, Quaternion.identity).GetComponent<ExitDoor>();
        }

    }
}
