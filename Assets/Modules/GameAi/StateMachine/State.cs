using System.Collections;
using UnityEngine;

namespace GameAi.StateMachine
{
    public abstract class State
    {
        protected StateMachine StateMachine;

        public State(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public virtual IEnumerator Start()
        {
            yield break;
        }

        // Should handle slow walk and Running
        public virtual IEnumerator Move()
        {
            Debug.Log("Normal move");

            yield break;
        }

        public virtual IEnumerator Attack()
        {
            yield break;
        }
    }
}