using UnityEngine;

public class EnemyBullet : Reusable
{
    [Range(100, 500)]
    [SerializeField] private int speed = 150;
    [Range(5, 50)]
    [SerializeField] private int strengthDamage = 25;
    private Rigidbody rb;
    private new SphereCollider collider;
    private Player player;
    private const string PlayerTag = "Player";
    private const string pauseManagerTag = "PauseManager";
    private bool isNotTeleport = true;
    private Vector3 newPosition;
    private const float nearTeleportZone = 0.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<Player>();
        player.Die += Die;
        GameObject.FindGameObjectWithTag(pauseManagerTag).GetComponent<Pause>().RestartGame += Die;
    }

    private void Update()
    {
        if (isNotTeleport)
        {
            rb.velocity = (player.transform.position - transform.position).normalized * speed * Time.deltaTime;
            transform.LookAt(player.transform.position);
        }
        else
        {
            rb.velocity = (newPosition - transform.position).normalized * speed * Time.deltaTime;
            transform.LookAt(newPosition);

            if ((newPosition - transform.position).magnitude < nearTeleportZone)
                Reuse(this);
        }
    }

    public void SetNewPosition(Vector3 newPosition)
    {
        isNotTeleport = false;
        collider.enabled = false;
        this.newPosition = newPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTag))
            player.StrengtLess(strengthDamage);

        Reuse(this);
    }

    private void Die()
    {
        Reuse(this);
    }
}
