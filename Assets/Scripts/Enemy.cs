using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    private GameManager gm;
    private Player player;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
    }

    public void Damage(int damage, int strengtUp)
    {
        health -= damage;

        if (health <= 0)
        {
            gm.ScoreUp();
            player.StrengtUp(strengtUp);
            Destroy(gameObject);
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
