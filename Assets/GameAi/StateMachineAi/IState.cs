namespace LockdownGames.GameAi.StateMachineAi
{
    public interface IState
    {
        void SetState(StateMachine sm);

        int Hash { get; }

        void Start();

        void Update();

        void FixedUpdate();

        void End();
    }
}