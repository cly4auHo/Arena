using UnityEngine;

public class Bullet : Reusable
{
    [Range(100, 500)]
    [SerializeField] private float speed = 300f;
    [Range(0, 50)]
    [SerializeField] private int damage = 25;
    [Range(0, 25)]
    [SerializeField] private int healing = 15;
    [Range(0, 20)]
    [SerializeField] private int strengthUpRicochet = 10;
    private Rigidbody rb;
    private Player player;
    private const string EnemyTag = "Enemy";
    private const string playerTag = "Player";
    private const string pauseManagerTag = "PauseManager";
    private int chanseOfRicochet;
    private bool isRicochet;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag(playerTag).GetComponent<Player>();
        chanseOfRicochet = Random.Range(0, player.FullHealth + 1);
        player.Die += Die;
        GameObject.FindGameObjectWithTag(pauseManagerTag).GetComponent<Pause>().RestartGame += Die;
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
            enemy.Damage(damage);

            if (!isRicochet)
            {
                Ricochet();
                return;
            }
            else if (isRicochet && enemy.Health <= 0)
            {
                HealOrStrengtUp();
            }
        }

        Die();
    }

    private void Ricochet()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);

        if (player.Health > chanseOfRicochet || enemies.Length == 0)
        {
            Die();
            return;
        }

        isRicochet = true;
        transform.LookAt(ChoseNearestTarget(enemies));
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
        if (Random.Range(0, 2) == 0)
            player.Healing(healing);
        else
            player.StrengtUp(strengthUpRicochet);
    }

    private void Die()
    {
        Reuse(this);
    }
}
