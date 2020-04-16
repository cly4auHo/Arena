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

    public void Damage(int damage, int strengtUp)
    {
        health -= damage;

        if (health <= 0)
            Die(strengtUp);
    }

    public int GetStrengthUp()
    {
        return strengthUpIfDie;
    }

    private void Die(int strengtUp)
    {
        gm.ScoreUp();
        player.StrengtUp(strengtUp);
        Destroy(gameObject);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public int GetHealth()
    {
        return health;
    }
}
