using Cinemachine;
using GameAi.Code.Player;
using GameCode.GameAi;
using GameCode.Messages;
using GameCode.MessagingFramework;
using GameCode.Models;
using SpelunkyLevelGen.LevelGenerator.LevelObjects.ExitDoor;
using SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomScripts;
using System;
using System.Collections;
using UnityEngine;

namespace GameCode
{
    public class GameManager : MonoBehaviour
    {
        public LevelGenerator LevelGenerator;
        public ALevelEnemiesPlacer LevelEnemiesPlacer;
        public GenPathFinder GenPathFinder;

        public GameObject player;
        public ExitDoor ExitDoor;

        public CinemachineVirtualCamera CVcam;

        private GameState currentState;
        private LevelData _levelData;

        public static event Action<GameState> OnGameStateChange;

        private void Awake()
        {
            MessageBus.Register<PlayerHealthUpdateMessage>(OnPlayerHealthUpdate);
            MessageBus.Register<PlayerWonMessage>(OnPlayerWin);

        }

        private void OnDestroy()
        {
            MessageBus.Remove<PlayerHealthUpdateMessage>(OnPlayerHealthUpdate);
            MessageBus.Remove<PlayerWonMessage>(OnPlayerWin);
        }

        private void OnPlayerWin(TransportMessage msg)
        {
            if (currentState != GameState.Running)
            {
                return;
            }

            currentState = GameState.Won;
        }

        private void OnPlayerHealthUpdate(TransportMessage trMessage)
        {
            var message = trMessage.ConvertTo<PlayerHealthUpdateMessage>();

            if (currentState == GameState.Running && message.HasPlayerDied)
            {
                currentState = GameState.Lost;
            }
        }

        private void Start()
        {
            currentState = GameState.Running;
            MessageBus.Publish(new GameStateUpdateMessage(currentState));

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

            CVcam.Follow = player.transform;

            ExitDoor = InitializeEndRoom(_levelData);

            var totalRooms = _levelData.LevelSize.x * _levelData.LevelSize.y;
            LevelEnemiesPlacer.PlaceEnemiesAsPerDifficulty(_levelData, totalRooms - 1);
        }

        private LevelData GenerateAndRenderLevel()
        {
            _levelData = new LevelData();
            _levelData = LevelGenerator.GenerateLevel(_levelData);

            
            return _levelData;
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

            Vector2 doorPosition = new Vector2(endRoom.transform.position.x, endRoom.transform.position.y - (endRoom.roomSize.y / 2) + 0.5f);
                
            return Instantiate(ExitDoor, doorPosition, Quaternion.identity).GetComponent<ExitDoor>();
        }

    }
}
