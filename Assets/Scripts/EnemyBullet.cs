using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private int strengthDamage = 25;

    private Rigidbody rb;
    private new SphereCollider collider;
    private Coroutine attack;

    private Player player;
    private const string PlayerTag = "Player";

    private Transform target;
    private bool isNotTeleport = true;
    private Vector3 newPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<SphereCollider>();

        target = GameObject.FindGameObjectWithTag(PlayerTag).transform;
        isNotTeleport = true;
        attack = StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        while (isNotTeleport)
        {
            yield return rb.velocity = (target.position - transform.position).normalized * speed;
            transform.LookAt(target.position);
        }

        StartCoroutine(Walk());
    }

    private IEnumerator Walk()
    {
        StopCoroutine(attack);

        while ((newPosition - transform.position).magnitude < 0.1f)
        {
            yield return rb.velocity = (newPosition - transform.position).normalized * speed;
            transform.LookAt(newPosition);
        }

        Destroy(gameObject);
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
