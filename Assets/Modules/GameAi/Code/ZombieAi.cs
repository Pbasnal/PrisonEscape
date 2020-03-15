using GameAi.ZombieStates;

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

    public void TakeDamage(int attackDamage)
    {
        CurrentHealth -= attackDamage;

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
