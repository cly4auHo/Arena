using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private int strengthDamage = 25;

    private Rigidbody rb;
    private new SphereCollider collider;

    private Player player;
    private Vector3 target;
    private const string PlayerTag = "Player";

    private bool isNotTeleport = true;
    private Vector3 newPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<SphereCollider>();

        player = FindObjectOfType<Player>();
        isNotTeleport = true;
    }

    private void Update()
    {
        if (isNotTeleport)
        {
            target = player.transform.position;
            rb.velocity = (target - transform.position).normalized * speed;
            transform.LookAt(target);
        }
        else
        {
            rb.velocity = (newPosition - transform.position).normalized * speed;
            transform.LookAt(newPosition);

            if ((newPosition - transform.position).magnitude < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void AfterTeleport(Transform newPosition)
    {
        isNotTeleport = false;
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
