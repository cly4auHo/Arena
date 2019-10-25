using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private int strengthDamage = 25;
    private Rigidbody rb;
    private SphereCollider collider;

    private Player player;
    private Transform target;
    private Vector3 targetPosition;
    private const string PlayerTag = "Player";

    private bool isTeleport = false;
    Vector3 newPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<SphereCollider>();
        target = GameObject.FindGameObjectWithTag(PlayerTag).transform;

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

            if ((newPosition - transform.position).magnitude < 0.5f) //near of tp zone
            {
                Destroy(gameObject);
            }
        }
    }

    public void AfterTeleport(Transform newPosition)
    {
        isTeleport = true;
        collider.enabled = false;
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
