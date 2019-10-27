using UnityEngine;

public class RedEnemy : Enemy
{
    [SerializeField] private float speed = 2f;
    private int damage = 15;

    private Vector3 myPosition;
    private Transform playerPosition;
    private Rigidbody rb;

    private Player player;
    private const string playerTag = "Player";

    private Transform currentPosition;
    private float jumpHight = 3.5f;
    private float timeOut = 5f;
    private float timer;

    void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag(playerTag).transform;
        rb = GetComponent<Rigidbody>();
        currentPosition = GetComponent<Transform>();

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
        if (currentPosition.position.y < jumpHight)
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
        myPosition = playerPosition.position;
        rb.velocity = (myPosition - transform.position).normalized * speed;
        transform.LookAt(myPosition);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            player = other.GetComponent<Player>();

            if (playerPosition)
            {
                player.Damage(damage);
            }

            Destroy(gameObject);
        }
    }
}
