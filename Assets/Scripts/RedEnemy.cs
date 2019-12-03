using UnityEngine;
using System.Collections;

public class RedEnemy : Enemy
{
    [SerializeField] private float speed = 2f;
    private int damage = 15;

    private Coroutine wait;
    private Rigidbody rb;
    private float jumpHight = 3.5f;
    private float timeOut = 5f;
    private float timer;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        timer = Time.timeSinceLevelLoad;

        wait = StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        while (Time.timeSinceLevelLoad - timer < timeOut)
        {
            yield return
                rb.velocity = (transform.position.y < jumpHight) ? Vector3.up * speed : rb.velocity = Vector3.zero;
        }

        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        StopCoroutine(wait);

        while (true)
        {
            yield return rb.velocity = (playerPosition.position - transform.position).normalized * speed;
            transform.LookAt(playerPosition.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            player.Damage(damage);
            Destroy(gameObject);
        }
    }
}
