using UnityEngine;

namespace GameCode.GameAi
{
    public class EnemyCollection : MonoBehaviour
    {
        public ZombieAi[] Enemies;

        public ZombieAi GetAnEnemy()
        {
            if (Enemies == null || Enemies.Length == 0)
            {
                throw new System.Exception("Enemies not set");
            }

            return Enemies[0];
        }
    }
}