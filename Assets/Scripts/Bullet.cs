using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5.5f;
    [SerializeField] private int damage = 25;

    private BlueEnemy blueEnemy;
    private RedEnemy redEnemy;
    private Player player;
    private GameManager gm;
    private int chanse;
    private int redStrengthUp = 15;
    private int blueStrengthUp = 50;
    private const string RedEnemyTag = "RedEnemy";
    private const string BlueEnemyTag = "BlueEnemy";

    void Start()
    {
        chanse = Random.Range(0, 100);
        gm = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(RedEnemyTag))
        {
            redEnemy = other.GetComponent<RedEnemy>();

            if (redEnemy && redEnemy.GetHealth() > damage)
            {
                redEnemy.Damage(damage);
            }
            else
            {
                Destroy(other.gameObject);
                gm.ScoreUp();
                player.SetStrengt(Mathf.Min(player.GetStrengt() + redStrengthUp, 100));
            }

            Destroy(gameObject);
        }
        else if (other.CompareTag(BlueEnemyTag))
        {
            blueEnemy = other.GetComponent<BlueEnemy>();

            if (blueEnemy && blueEnemy.GetHealth() > damage)
            {
                blueEnemy.Damage(damage);
            }
            else
            {
                Destroy(other.gameObject);
                gm.ScoreUp();
                player.SetStrengt(Mathf.Min(player.GetStrengt() + blueStrengthUp, 100));
            }

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
