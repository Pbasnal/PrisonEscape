namespace LockdownGames.GameCode.Messages
{
    public class PlayerHealthUpdateMessage
    {
        public float Playerhealth;

        public bool HasPlayerDied => Playerhealth == 0;

        public PlayerHealthUpdateMessage(float health)
        {
            Playerhealth = health;
        }
    }
}
