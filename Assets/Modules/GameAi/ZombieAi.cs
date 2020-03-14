using GameAi.StateMachine;
using UnityEngine;

public class ZombieAi : StateMachine
{
    // Start is called before the first frame update
    void Start()
    {
        SetState(new WanderingState(this));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update " + State.GetType().Name);

        StartCoroutine(State.Move());
    }
}
