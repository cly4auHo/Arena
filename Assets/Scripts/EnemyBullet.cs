using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private int strengthDamage = 25;

    private Rigidbody rb;
    private new SphereCollider collider;

    private Player player;
    private const string PlayerTag = "Player";

    private Transform target;
    private Vector3 targetPosition;

    private bool isTeleport = false;
    private Vector3 newPosition;

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
            Attack();
        }
        else
        {
            Walk();

            if ((newPosition - transform.position).magnitude < 0.1f) //near of tp zone
            {
                Destroy(gameObject);
            }
        }
    }

    void Attack()
    {
        targetPosition = target.position;
        rb.velocity = (targetPosition - transform.position).normalized * speed;
        transform.LookAt(targetPosition);
    }

    void Walk()
    {
        rb.velocity = (newPosition - transform.position).normalized * speed;
        transform.LookAt(newPosition);
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

            player.StrengtLess(strengthDamage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
