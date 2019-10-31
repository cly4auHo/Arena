using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 25;

    private Player player;
    private int fullHp = 100;

    private int chanseOfRicochet;
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

            red.Damage(damage, redStrengthUp);

            if (!isRicochet)
            {
                Ricochet();
            }
            else if (isRicochet && red.GetHealth() <= 0) //if ricochet kill
            {
                HealOrStrengtUp();
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        else if (other.gameObject.name == "BlueEnemy(Clone)")
        {
            BlueEnemy blue = other.GetComponent<BlueEnemy>();

            blue.Damage(damage, blueStrengthUp);

            if (!isRicochet)
            {
                Ricochet();
            }
            else if (isRicochet && blue.GetHealth() <= damage)
            {
                HealOrStrengtUp();
                Destroy(gameObject);
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
        if (player.GetHealth() > chanseOfRicochet) //than less hp than more chance of ricochet
        {
            Destroy(gameObject);
            return;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);

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