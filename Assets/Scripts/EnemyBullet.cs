using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private int strengthDamage = 25;
    private Transform target;
    private Vector3 targetPosition;
    private Rigidbody rb;

    private const string PlayerTag = "Player";
    private Player player;
    private bool isTeleport = false;

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
            transform.LookAt(targetPosition);
            rb.velocity = (targetPosition - transform.position).normalized * speed;

            if (transform.position == targetPosition)
            {
                Destroy(gameObject);
            }
        }
    }

    public void AfterTeleport(Vector3 newPosition)
    {
        isTeleport = true;
        targetPosition = newPosition;
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
