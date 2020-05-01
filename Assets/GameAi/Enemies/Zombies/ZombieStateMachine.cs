using System.Collections.Generic;

using LockdownGames.GameCode.GameAi.StateMachine2;

namespace LockdownGames.GameAi.Enemies.Zombies
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
