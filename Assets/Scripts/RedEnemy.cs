using UnityEngine;

public class RedEnemy : Enemy
{
    [SerializeField] private float speed = 2f;
    private int damage = 15;

    private Rigidbody rb;
    private float jumpHight = 3.5f;
    private float timeOut = 5f;
    private float timer;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        timer = Time.timeSinceLevelLoad;
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad - timer < timeOut)
        {
            Wait();
        }
        else
        {
            Attack();
        }
    }

    void Wait()
    {
        if (transform.position.y < jumpHight)
        {
            rb.velocity = Vector3.up * speed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    void Attack()
    {
        rb.velocity = (playerPosition.position - transform.position).normalized * speed;
        transform.LookAt(playerPosition.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Player player = other.GetComponent<Player>();

            player.Damage(damage);
            Destroy(gameObject);
        }
    }
}
