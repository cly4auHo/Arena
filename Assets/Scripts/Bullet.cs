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

    private void Ricochet()
    {
        redEnemies = FindObjectsOfType<RedEnemy>();
        blueEnemies = FindObjectsOfType<BlueEnemy>();

        if (redEnemies.Length == 0 || blueEnemies.Length == 0)
        {
            Destroy(gameObject);
            return;
        }

        ChoseNearestTarget(redEnemies, blueEnemies);
    }

    private void ChoseNearestTarget(RedEnemy[] RedEnemies, BlueEnemy[] BlueEnemies)
    {
        Transform nearestTargetRed = RedEnemies[0].transform;
        Transform nearestTargetBlue = BlueEnemies[0].transform;
        int nearestRed = 0;
        int nearestBlue = 0;

        Vector3 currentPosition = transform.position; // for bullet in this time
        float closestDistanceSqr = Mathf.Infinity;

        for (int i = 0; i < RedEnemies.Length - 1; i++) // chose nearest redEnemy
        {
            Vector3 directionToTarget = RedEnemies[i].transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                nearestTargetRed = RedEnemies[i].transform;
                nearestRed = i;
            }
        }

        for (int i = 0; i < BlueEnemies.Length - 1; i++) //chose nearest blueEnemy
        {
            Vector3 directionToTarget = BlueEnemies[i].transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                nearestTargetBlue = BlueEnemies[i].transform;
                nearestBlue = i;
            }
        }

        if (nearestTargetBlue.position.sqrMagnitude < nearestTargetRed.position.sqrMagnitude)
        {
            if (BlueEnemies[nearestBlue].GetHealth() > damage)
            {
                BlueEnemies[nearestBlue].SetHealth(BlueEnemies[nearestBlue].GetHealth() - damage);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
                Destroy(BlueEnemies[nearestBlue]);

                player.ScoreUp();
                player.SetStrengt(Mathf.Min(player.GetStrengt() + blueStrengthUp, fullStrengt));
                HealOrStrengtUp();
            }
        }
        else
        {
            if (RedEnemies[nearestRed].GetHealth() > damage)
            {
                RedEnemies[nearestRed].SetHealth(RedEnemies[nearestRed].GetHealth() - damage);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
                Destroy(RedEnemies[nearestRed]);

                player.ScoreUp();
                player.SetStrengt(Mathf.Min(player.GetStrengt() + redStrengthUp, fullStrengt));
                HealOrStrengtUp();
            }
        }
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