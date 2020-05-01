using System.Collections.Generic;

using UnityEngine;

namespace LockdownGames.GameCode.GameAi.StateMachine2
{
    public abstract class StateMachine : MonoBehaviour
    {
        public IState currentState { get; private set; }

        private IDictionary<int, IState> stateMap;

        public void InitializeStateMachine<T>(List<IState> states, T startingState) where T : IState
        {
            stateMap = new Dictionary<int, IState>();

            foreach (var state in states)
            {
                stateMap.Add(state.Hash, state);
            }

            SwitchToStateWithoutEndingPrevious<T>();
        }

        public void SetStateTo<T>(T state) where T : IState
        {
            currentState.End();

            SwitchToStateWithoutEndingPrevious<T>();
        }

        public void SetStateTo<T>() where T : IState
        {
            currentState.End();

            SwitchToStateWithoutEndingPrevious<T>();
        }

        private IState GetState<T>() where T : IState
        {
            var hash = typeof(T).GetHashCode();

            if (!stateMap.TryGetValue(hash, out var newState))
            {
                throw new UnityException("StateMachine of " + name + " Cannot transition to state " + typeof(T).Name + " because that does not exists.");
            }

            return newState;
        }

        protected void SwitchToStateWithoutEndingPrevious<T>() where T : IState
        {
            var newState = GetState<T>();

            currentState = newState;
            currentState.Start();
        }
    }
}
