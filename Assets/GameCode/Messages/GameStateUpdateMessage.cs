using GameCode.MessagingFramework;
using System.Collections;


namespace GameCode
{
    public enum GameState
    {
        NotStated,
        Running,
        Won,
        Lost
    }

    public class GameStateUpdateMessage : IMessage
    {
        public GameState GameState;

        public GameStateUpdateMessage(GameState state)
        {
            GameState = state;
        }
    }
}
