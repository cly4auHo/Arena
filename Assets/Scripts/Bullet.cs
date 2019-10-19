using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5.5f;
    [SerializeField] private int damage = 25;

    private BlueEnemy blueEnemy;
    private RedEnemy redEnemy;

    private const string RedEnemyTag = "RedEnemy";
    private const string BlueEnemyTag = "BlueEnemy";

    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(RedEnemyTag))
        {
            redEnemy = other.GetComponent<RedEnemy>();
            if (redEnemy)
            {
                redEnemy.Damage(damage);              
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag(BlueEnemyTag))
        {
            blueEnemy = other.GetComponent<BlueEnemy>();
            if (blueEnemy)
            {
                blueEnemy.Damage(damage);               
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
