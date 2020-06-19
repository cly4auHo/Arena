using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Range(50, 200)]
    [SerializeField] protected int health;
    [Range(0, 50)]
    [SerializeField] protected int strengthUpIfDie = 25;
    protected Player player;
    protected Vector3 playerPosition;
    protected const string playerTag = "Player";

    private GameManager gm;

    protected void Start()
    {
        gm = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
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

    public int GetHealth()
    {
        return health;
    }

    private void Die()
    {
        gm.ScoreUp();
        player.StrengtUp(strengthUpIfDie);
        Destroy(gameObject);
    }
}
