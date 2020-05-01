namespace LockdownGames.GameCode.GameAi.StateMachine2
{
    public interface IState
    {
        int Hash { get; }

        void Start();

        void Update();

        void End();
    }

    public abstract class State<T> : IState where T : StateMachine
    {
        protected T stateMachine;
        public int Hash { get; private set; }

        public State(T stateMachine)
        {
            Hash = this.GetType().GetHashCode();

            this.stateMachine = stateMachine;
        }

        public abstract void End();

        public abstract void Start();

        public abstract void Update();
    }
}