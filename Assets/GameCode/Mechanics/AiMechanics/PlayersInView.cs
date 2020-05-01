using UnityEngine;

namespace LockdownGames.GameCode.Mechanics.AiMechanics
{
    public class PlayerInView
    {
        public bool IsPlayersInView { get; private set; }
        public Transform PlayerTransform { get; private set; }
        public float DistanceFromPlayer { get; private set; }

        public PlayerInView()
        {
            IsPlayersInView = false;
        }

        public void AddPlayerInfo(Transform playerTransform, float distance)
        {
            IsPlayersInView = true;
            PlayerTransform = playerTransform;
            DistanceFromPlayer = distance;
        }

        public void ClearPlayer()
        {
            IsPlayersInView = false;
        }
    }
}
