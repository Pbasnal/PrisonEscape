using System.Collections;
using UnityEngine;

namespace LockdownGames.GameCode.GameAi.Code.FiniteStateMachine
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
        public virtual IEnumerator ProcessState()
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