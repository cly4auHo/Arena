using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    protected Transform playerPosition;
    protected Player player;

    private GameManager gm;
    protected const string playerTag = "Player";

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();

        playerPosition = player.transform;
    }

    public void Damage(int damage, int strengtUp)
    {
        health -= damage;

        if (health <= 0)
        {
            Die(strengtUp);
        }
    }

    private void Die(int strengtUp)
    {
        gm.ScoreUp();
        player.StrengtUp(strengtUp);
        Destroy(gameObject);
    }

    public int GetHealth()
    {
        return health;
    }
}
