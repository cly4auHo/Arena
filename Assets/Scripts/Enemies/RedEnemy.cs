using UnityEngine;
using System.Collections;

public class RedEnemy : Enemy
{
    [Header("Personal characteristics")]
    [Range(50, 200)]
    [SerializeField] private float speed = 2f;
    [Range(5, 20)]
    [SerializeField] private int damage = 15;

    private Coroutine wait;
    private Rigidbody rb;
    private float jumpHight = 3.5f;
    private float timeOut = 5f;
    private float timerSpawn;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        timerSpawn = Time.timeSinceLevelLoad;
        wait = StartCoroutine(Wait());
    }

    private void Update()
    {
        playerPosition = player.transform.position;
    }

    private IEnumerator Wait()
    {
        while (Time.timeSinceLevelLoad - timerSpawn < timeOut)
        {
            yield return
                rb.velocity = (transform.position.y < jumpHight) ? Vector3.up * speed * Time.deltaTime : Vector3.zero;
        }

        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        StopCoroutine(wait);

        while (true)
        {
            yield return rb.velocity = (playerPosition - transform.position).normalized * speed * Time.deltaTime;
            transform.LookAt(playerPosition);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            player.Damage(damage);
            Destroy(gameObject);
        }
    }
}
