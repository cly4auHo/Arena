using UnityEngine;

public abstract class Enemy : Reusable
{
    [Range(50, 200)]
    [SerializeField] protected int health;
    [Range(0, 50)]
    [SerializeField] protected int strengthUpIfDie = 25;
    protected Player player;
    protected Vector3 playerPosition;
    protected PoolManager poolManager;
    protected const string playerTag = "Player";
    private const string gmTag = "GameManager";
    private const string pmTag = "PoolManager";
    private const string pauseManagerTag = "PauseManager";
    private GameManager gm;

    public int Health => health;

    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).GetComponent<Player>();
        player.Ult += Die;
        player.Die += Die;
        gm = GameObject.FindGameObjectWithTag(gmTag).GetComponent<GameManager>();
        poolManager = GameObject.FindGameObjectWithTag(pmTag).GetComponent<PoolManager>();
        GameObject.FindGameObjectWithTag(pauseManagerTag).GetComponent<Pause>().RestartGame += Die;
    }

    protected void Update()
    {
        playerPosition = player.transform.position;
    }

    public void Damage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        gm.ScoreUp();
        player.StrengtUp(strengthUpIfDie);
        Reuse?.Invoke(this);
    }
}
