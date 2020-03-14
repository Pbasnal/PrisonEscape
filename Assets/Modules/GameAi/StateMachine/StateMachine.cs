using UnityEngine;

namespace GameAi.StateMachine
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected State State;

        public void SetState(State state)
        {
            this.State = state;

            StartCoroutine(this.State.Start());
        }
    }
}
