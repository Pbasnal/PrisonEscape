namespace LockdownGames.GameAi.StateMachineAi
{

    public abstract class State<T> : IState where T : StateMachine
    {
        protected T stateMachine;
        public int Hash { get; private set; }

        //public State(T stateMachine)
        //{
        //    SetState(stateMachine);
        //}

        public virtual void SetState(StateMachine sm)
        {
            this.stateMachine = (T)sm;
            Hash = this.GetType().GetHashCode();
        }

        public abstract void End();

        public abstract void Start();

        public abstract void Update();

        public abstract void FixedUpdate();
    }
}