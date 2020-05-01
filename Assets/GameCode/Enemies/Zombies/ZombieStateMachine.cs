using GameAi.StateMachine2;
using LockdownGames.GameCode.Enemies.Zombies.States;
using System.Collections.Generic;

namespace LockdownGames.GameCode.Enemies.Zombies
{
    public class ZombieStateMachine : StateMachine
    {
        private void Awake()
        {
            var startingState = new WanderingState(this);
            
            InitializeStateMachine(new List<IState>
            {
                startingState
            }, startingState);
        }

        private void Update()
        {
            currentState.Update();
        }
    }
}
