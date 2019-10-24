using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 25;

    private BlueEnemy blueEnemy;
    private RedEnemy redEnemy;
    private Player player;

    private int chanseOfRicochet;
    private int healing = 50;
    private int strengthUpRicochet = 10;

    private const string RedEnemyTag = "RedEnemy";
    private const string BlueEnemyTag = "BlueEnemy";
    private int redStrengthUp = 15;
    private int blueStrengthUp = 50;
    private int fullHp = 100;
    private int fullStrengt = 100;

    void Start()
    {
        chanseOfRicochet = Random.Range(0, fullHp);
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
                player.ScoreUp();
                player.SetStrengt(Mathf.Min(player.GetStrengt() + redStrengthUp, fullStrengt));
            }

            if (player.GetHealth() < chanseOfRicochet) //than less hp, than more chance to ricochet
            {
                Ricochet();
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
                player.ScoreUp();
                player.SetStrengt(Mathf.Min(player.GetStrengt() + blueStrengthUp, fullStrengt));
            }

            if (player.GetHealth() < chanseOfRicochet)
            {
                Ricochet();
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

    void Ricochet()
    {
        GameObject[] RedEnemies = GameObject.FindGameObjectsWithTag(RedEnemyTag);
        GameObject[] BlueEnemies = GameObject.FindGameObjectsWithTag(BlueEnemyTag);

        if (RedEnemies.Length == 0 || BlueEnemies.Length == 0)
        {
            Destroy(gameObject);
            return;
        }

        transform.LookAt(ChoseNearestTarget(RedEnemies, BlueEnemies));

        int chanseOfHeal = Random.Range(0, 1);

        if (chanseOfHeal == 0) //healing or strengtUp
        {
            player.SetHealth(Mathf.Min(player.GetHealth() + healing, fullHp));
        }
        else
        {
            player.SetStrengt(Mathf.Min(player.GetStrengt() + strengthUpRicochet, fullStrengt));
        }
    }

    private Transform ChoseNearestTarget(GameObject[] RedEnemies, GameObject[] BlueEnemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;


        foreach (GameObject potentialTarget in RedEnemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }
        foreach (GameObject potentialTarget in BlueEnemies)
        {
            bestTarget.position = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = bestTarget.position.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }

        return bestTarget;
    }
}