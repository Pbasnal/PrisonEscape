namespace LockdownGames.GameAi.StateMachineAi
{
    public interface IState
    {
        int Hash { get; }

        void Start();

        void Update();

        void End();
    }
}