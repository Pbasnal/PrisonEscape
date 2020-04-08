using GameCode.MessagingFramework;

namespace GameCode.Messages
{
    public class PlayerHealthUpdateMessage : IMessage
    {
        public int Playerhealth;

        public bool HasPlayerDied => Playerhealth == 0;

        public PlayerHealthUpdateMessage(int health)
        {
            Playerhealth = health;
        }
    }
}
