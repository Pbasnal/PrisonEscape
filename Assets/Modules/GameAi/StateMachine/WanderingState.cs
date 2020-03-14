using System.Collections;
using UnityEngine;

namespace GameAi.StateMachine
{
    public class WanderingState : State
    {
        public WanderingState(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override IEnumerator Move()
        {
            Debug.Log("Calling wandering move");

            var pos = StateMachine.transform.position;
            StateMachine.transform.position = new Vector2(pos.x, pos.y + 2 * Time.deltaTime);

            yield return null;
        }
    }
}
