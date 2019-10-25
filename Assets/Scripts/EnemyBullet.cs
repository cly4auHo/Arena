using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private int strengthDamage = 25;
    private Rigidbody rb;

    private Player player;
    private Transform target;
    private Vector3 targetPosition;
    private const string PlayerTag = "Player";

    private bool isTeleport = false;
    Vector3 newPosition;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag(PlayerTag).transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isTeleport)
        {
            targetPosition = target.position;
            rb.velocity = (targetPosition - transform.position).normalized * speed;
            transform.LookAt(targetPosition);
        }
        else
        {
            rb.velocity = (newPosition - transform.position).normalized * speed;
            transform.LookAt(newPosition);

            if (transform.position == newPosition)
            {
                Destroy(gameObject);
            }
        }
    }

    public void AfterTeleport(Transform newPosition)
    {
        isTeleport = true;
        this.newPosition = newPosition.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            player = other.GetComponent<Player>();

            if (player)
            {
                player.SetStrengt(Mathf.Max(player.GetStrengt() - strengthDamage, 0));
            }

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
