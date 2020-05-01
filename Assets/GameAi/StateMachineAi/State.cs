namespace LockdownGames.GameAi.StateMachineAi
{

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