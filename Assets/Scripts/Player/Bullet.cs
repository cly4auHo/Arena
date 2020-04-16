using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Characteristics")]
    [Range(100, 500)]
    [SerializeField] private float speed = 300f;
    [Range(0, 50)]
    [SerializeField] private int damage = 25;
    private Rigidbody rb;
    private Player player;
    private const string EnemyTag = "Enemy";

    [Range(0, 100)]
    [SerializeField] private int healing = 15;
    [Range(0, 100)]
    [SerializeField] private int strengthUpRicochet = 10;
    private int chanseOfRicochet;
    private bool isRicochet;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isRicochet = false;
        player = FindObjectOfType<Player>();
        chanseOfRicochet = Random.Range(0, player.GetFullHealth());
    }

    private void Update()
    {
        rb.velocity = transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(EnemyTag))
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy == null)
            {
                Destroy(gameObject);
                return;
            }

            enemy.Damage(damage, enemy.GetStrengthUp());

            if (!isRicochet)
                Ricochet();
            else if (isRicochet && enemy.GetHealth() <= 0) //if it ricochet, it will kill
            {
                HealOrStrengtUp();
                Destroy(gameObject);
            }
            else
                Destroy(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Ricochet()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);

        if (player.GetHealth() > chanseOfRicochet || enemies.Length == 0) //than less hp than more chance of ricochet
        {
            Destroy(gameObject);
            return;
        }

        isRicochet = true;
        gameObject.transform.LookAt(ChoseNearestTarget(enemies));
    }

    private Transform ChoseNearestTarget(GameObject[] enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject potentialTarget in enemies)
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
        int chanseOfHeal = Random.Range(0, 2);

        if (chanseOfHeal == 0)
            player.Healing(healing);
        else
            player.StrengtUp(strengthUpRicochet);
    }
}
