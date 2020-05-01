namespace LockdownGames.GameCode.Messages
{
    public enum GameState
    {
        NotStated,
        Running,
        Won,
        Lost
    }

    public class GameStateUpdateMessage
    {
        public GameState GameState;

        public GameStateUpdateMessage(GameState state)
        {
            GameState = state;
        }
    }
}
