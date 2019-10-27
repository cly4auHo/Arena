using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    private GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    public void Damage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            gm.ScoreUp();
            Destroy(gameObject);
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
