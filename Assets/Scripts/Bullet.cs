using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 25;

    private Player player;
    private GameObject[] enemies;

    private int chanseOfRicochet;
    private int fullHp = 100;
    private bool isRicochet = false;

    private int healing = 50;
    private int strengthUpRicochet = 10;

    private const string EnemyTag = "Enemy";
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
        if (other.gameObject.name == "RedEnemy(Clone)")
        {
            RedEnemy red = other.GetComponent<RedEnemy>();

            if (!isRicochet)
            {
                red.Damage(damage, redStrengthUp);
                Ricochet();
            }
            else if (isRicochet && red.GetHealth() <= damage)
            {
                red.Damage(damage, redStrengthUp);
                Destroy(gameObject);

                HealOrStrengtUp();
            }
            else
            {
                red.Damage(damage, redStrengthUp);
                Destroy(gameObject);
            }
        }

        else if (other.gameObject.name == "BlueEnemy(Clone)")
        {
            BlueEnemy blue = other.GetComponent<BlueEnemy>();

            if (!isRicochet)
            {
                blue.Damage(damage, blueStrengthUp);
                Ricochet();
            }
            else if (isRicochet && blue.GetHealth() <= damage)
            {
                blue.Damage(damage, blueStrengthUp);
                Destroy(gameObject);

                HealOrStrengtUp();
            }
            else
            {
                blue.Damage(damage, blueStrengthUp);
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
        if (player.GetHealth() > chanseOfRicochet) //than less hp than more chance of ricochet
        {
            Destroy(gameObject);
            return;
        }

        enemies = GameObject.FindGameObjectsWithTag(EnemyTag);

        if (enemies.Length == 0)
        {
            Destroy(gameObject);
            return;
        }

        isRicochet = true;
        gameObject.transform.LookAt(ChoseTarget(enemies));
    }

    private Transform ChoseTarget(GameObject[] Enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject potentialTarget in Enemies)
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
            player.Healing(healing);
        }
        else
        {
            player.StrengtUp(strengthUpRicochet);
        }
    }
}