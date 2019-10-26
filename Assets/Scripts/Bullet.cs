using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 25;

    private BlueEnemy blueEnemy; // for trigger
    private RedEnemy redEnemy;
    private Player player;

    private BlueEnemy[] blueEnemies; // for ricochet
    private RedEnemy[] redEnemies;

    private int chanseOfRicochet;
    private bool isRicochet = false;
    private int healing = 50;
    private int fullHp = 100;
    private int fullStrengt = 100;
    private int strengthUpRicochet = 10;

    private const string RedEnemyTag = "RedEnemy";
    private const string BlueEnemyTag = "BlueEnemy";
    private int redStrengthUp = 15;
    private int blueStrengthUp = 50;

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

                if (isRicochet)
                {
                    HealOrStrengtUp();
                }
            }

            Ricochet();
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

                if (isRicochet)
                {
                    HealOrStrengtUp();
                }
            }

            Ricochet();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Ricochet()
    {
        if (player.GetHealth() > chanseOfRicochet) //than less hp than more chance of ricochet
        {
            Destroy(gameObject);
            return;
        }

        redEnemies = FindObjectsOfType<RedEnemy>();
        blueEnemies = FindObjectsOfType<BlueEnemy>();

        if (redEnemies.Length == 0 || blueEnemies.Length == 0)
        {
            Destroy(gameObject);
            return;
        }

        isRicochet = true;
        gameObject.transform.LookAt(ChoseTarget(redEnemies, blueEnemies));
    }

    private Transform ChoseTarget(RedEnemy[] RedEnemies, BlueEnemy[] BlueEnemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (RedEnemy potentialTarget in RedEnemies) //nearest red
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }

        foreach (BlueEnemy potentialTarget in BlueEnemies) //nearest blue
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }

        return bestTarget;
    }

    private void HealOrStrengtUp()
    {
        int chanseOfHeal = Random.Range(0, 1);

        if (chanseOfHeal == 0)
        {
            player.SetHealth(Mathf.Min(player.GetHealth() + healing, fullHp));
        }
        else
        {
            player.SetStrengt(Mathf.Min(player.GetStrengt() + strengthUpRicochet, fullStrengt));
        }
    }
}