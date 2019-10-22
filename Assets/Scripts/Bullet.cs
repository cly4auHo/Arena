using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5.5f;
    [SerializeField] private int damage = 25;

    private BlueEnemy blueEnemy;
    private RedEnemy redEnemy;
    private Player player;
    private GameManager gm;

    private int chanseOfRicochet;
    private int healing = 50;
    private int strengthUpRicochet = 10;
    private int redStrengthUp = 15;
    private int blueStrengthUp = 50;

    private const string RedEnemyTag = "RedEnemy";
    private const string BlueEnemyTag = "BlueEnemy";

    private int chanseForRed = 0;
    private int chanseForBlue = 1;
    private int chanseForHealing = 1;
    private int fullHp = 100;
    private int fullStrengt = 100;

    void Start()
    {
        chanseOfRicochet = Random.Range(0, fullHp);

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
                redEnemy.SetHealth(redEnemy.GetHealth() - damage);
            }
            else
            {
                Destroy(other.gameObject);
                gm.ScoreUp();
                player.SetStrengt(Mathf.Min(player.GetStrengt() + redStrengthUp, fullStrengt));
            }

            if (player.GetHealth() < chanseOfRicochet)
            {
                int chanseRicochet = Random.Range(0, 1);
                int chanseOfHeal = Random.Range(0, 1);

                if (chanseRicochet == chanseForRed)
                {
                    GameObject[] RedEnemies = GameObject.FindGameObjectsWithTag(RedEnemyTag);

                    var goal = RedEnemies[Random.Range(0, RedEnemies.Length - 1)];
                    transform.LookAt(goal.transform);
                }
                else if (chanseRicochet == chanseForBlue)
                {
                    GameObject[] BlueEnemies = GameObject.FindGameObjectsWithTag(BlueEnemyTag);

                    var goal = BlueEnemies[Random.Range(0, BlueEnemies.Length - 1)];
                    transform.LookAt(goal.transform);
                }

                if (chanseOfHeal == chanseForHealing)
                {
                    player.SetHealth(Mathf.Min(player.GetHealth() + healing, fullHp));
                }
                else
                {
                    player.SetStrengt(Mathf.Min(player.GetStrengt() + strengthUpRicochet, fullStrengt));
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else if (other.CompareTag(BlueEnemyTag))
        {
            blueEnemy = other.GetComponent<BlueEnemy>();

            if (blueEnemy && blueEnemy.GetHealth() > damage)
            {
                blueEnemy.SetHealth(blueEnemy.GetHealth() - damage);
            }
            else
            {
                Destroy(other.gameObject);
                gm.ScoreUp();
                player.SetStrengt(Mathf.Min(player.GetStrengt() + blueStrengthUp, fullStrengt));
            }

            if (player.GetHealth() < chanseOfRicochet)
            {
                int chanseRicochet = Random.Range(0, 1);
                int chanseOfHeal = Random.Range(0, 1);

                if (chanseRicochet == chanseForRed)
                {
                    GameObject[] RedEnemies = GameObject.FindGameObjectsWithTag(RedEnemyTag);

                    var goal = RedEnemies[Random.Range(0, RedEnemies.Length - 1)];
                    transform.LookAt(goal.transform);
                }
                else if (chanseRicochet == chanseForBlue)
                {
                    GameObject[] BlueEnemies = GameObject.FindGameObjectsWithTag(BlueEnemyTag);

                    var goal = BlueEnemies[Random.Range(0, BlueEnemies.Length - 1)];
                    transform.LookAt(goal.transform);
                }

                if (chanseOfHeal == chanseForHealing)
                {
                    player.SetHealth(Mathf.Min(player.GetHealth() + healing, fullHp));
                }
                else
                {
                    player.SetStrengt(Mathf.Min(player.GetStrengt() + strengthUpRicochet, fullStrengt));
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
