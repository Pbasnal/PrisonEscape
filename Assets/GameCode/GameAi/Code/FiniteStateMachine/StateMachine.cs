﻿using UnityEngine;

namespace GameAi.FiniteStateMachine
{
    public abstract class StateMachine : MonoBehaviour
    {
        public State State { get; private set; }

        public void SetState(State state)
        {
            this.State = state;
            StartCoroutine(this.State.Start());
        }
    }
}
