using UnityEngine;
using System.Collections;

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
    private Coroutine wait;
    private Rigidbody rb;
    private float timerSpawn;

    private new void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        timerSpawn = Time.timeSinceLevelLoad;
        wait = StartCoroutine(Wait());
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
