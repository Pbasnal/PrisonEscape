using GameAi.Code;
using GameAi.ZombieStates;
using UnityEngine;

public class ZombieAi : ZombieStateMachine
{
    public int MaxHealth = 20;
    public int CurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        Initialize();
        SetState(new WanderingState(this));
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update " + State.GetType().Name);
        StartCoroutine(State.ProcessState());
    }

    public void TakeDamage(Transform attacker, int attackDamage)
    {
        CurrentHealth -= attackDamage;

        // need logic to start attacking player is player sneaks up from behind
        //lastKnownPlayerPosition = new PlayerInView();
        //lastKnownPlayerPosition.AddPlayerInfo(attacker, Vector2.Distance(transform.position, attacker.position));

        //SetState(new SearchLastKnownPosition(this));

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
