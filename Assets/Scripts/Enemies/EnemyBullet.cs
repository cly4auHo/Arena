using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Range(100, 500)]
    [SerializeField] private int speed = 150;
    [Range(5, 50)]
    [SerializeField] private int strengthDamage = 25;
    private Rigidbody rb;
    private new SphereCollider collider;

    private Player player;
    private Vector3 target;
    private const string PlayerTag = "Player";

    private bool isNotTeleport = true;
    private Vector3 newPosition;
    private float nearTeleportZone = 0.1f;

    private void Start()
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
            rb.velocity = (target - transform.position).normalized * speed * Time.deltaTime;
            transform.LookAt(target);
        }
        else
        {
            rb.velocity = (newPosition - transform.position).normalized * speed * Time.deltaTime;
            transform.LookAt(newPosition);

            if ((newPosition - transform.position).magnitude < nearTeleportZone)
                Destroy(gameObject);
        }
    }

    public void AfterTeleport(Transform newPosition)
    {
        isNotTeleport = false;
        collider.enabled = false;
        this.newPosition = newPosition.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            player = other.GetComponent<Player>();
            player.StrengtLess(strengthDamage);
            Destroy(gameObject);
        }
        else
            Destroy(gameObject);
    }
}
