using UnityEngine;

public class RedEnemy : Enemy
{
    [Header("Personal characteristics")]
    [Range(50, 200)]
    [SerializeField] private float speed = 2f;
    [Range(5, 20)]
    [SerializeField] private int damage = 15;
    [Range(0, 10)]
    [SerializeField] private float jumpHight = 3.5f;
    [Range(0, 10)]
    [SerializeField] private float timeOut = 5f;
    private Rigidbody rb;
    private float timerSpawn;

    private new void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        timerSpawn = Time.timeSinceLevelLoad;
    }

    private new void Update()
    {
        base.Update();

        if (Time.timeSinceLevelLoad - timerSpawn < timeOut)
        {
            rb.velocity = (transform.position.y < jumpHight) ? Vector3.up * speed * Time.deltaTime : Vector3.zero;
        }
        else
        {
            rb.velocity = (playerPosition - transform.position).normalized * speed * Time.deltaTime;
            transform.LookAt(playerPosition);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            player.Damage(damage);
            Reuse?.Invoke(this);
        }
    }
}
