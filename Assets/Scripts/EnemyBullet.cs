using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 0.3f;
    private int strengthDamage = 25;
    private Transform target;
    private Vector3 targetPosition;
    private Rigidbody rb;

    private const string PlayerTag = "Player";
    private Player player;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag(PlayerTag).transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        targetPosition = target.position;
        rb.velocity = (targetPosition - transform.position).normalized * speed;
        transform.LookAt(targetPosition);       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            player = other.GetComponent<Player>();
            if (player)
            {
                player.StrengthLess(strengthDamage);
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
