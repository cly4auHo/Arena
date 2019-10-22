using UnityEngine;

public class RedEnemy : MonoBehaviour
{
    private int health = 50;
    private int damage = 15;
    [SerializeField] private float speed = 5;

    private Vector3 playerPosition;
    private Transform player;
    private Rigidbody rb;
    private Player playerHit;
    private const string playerTag = "Player";

    private float timeOut = 5f;
    private float timer;
    private float jumpHight = 3;
    private float jumpSpeed = 5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).transform;
        rb = GetComponent<Rigidbody>();

        timer = Time.timeSinceLevelLoad;
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad - timer < timeOut)
        {

        }
        else
        {
            playerPosition = player.position;
            rb.velocity = (playerPosition - transform.position).normalized * speed;
            transform.LookAt(playerPosition);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerHit = other.GetComponent<Player>();

            if (player)
            {
                playerHit.SetHealth(Mathf.Min(playerHit.GetHealth() - damage, 0));
            }

            Destroy(gameObject);
        }
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public int GetHealth()
    {
        return health;
    }
}
