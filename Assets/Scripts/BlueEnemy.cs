using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemy : MonoBehaviour
{
    private int health = 100;
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private float obstacleRange = 0.3f;
    private float angle;

    [SerializeField] private GameObject enemyBulletPrefab;
    private GameObject currentBullet;

    private Ray ray;
    private float radiusOfRay = 1f;
    private GameObject hitObject;

    private Transform target;
    private Vector3 targetPosition;
    private Rigidbody rb;
    private const string TargetTag = "Player";

    private GameManager gm;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag(TargetTag).transform;
        rb = GetComponent<Rigidbody>();
        health = 100;
    }

    void Update()
    {



        //ray = new Ray(transform.position, transform.forward);
        //RaycastHit hit;

        //if (Physics.SphereCast(ray, radiusOfRay, out hit))
        //{
        //    hitObject = hit.transform.gameObject;

        //    if (hitObject.GetComponent<Player>())
        //    {
        //        StartCoroutine(Shoot());
        //    }
        //    else if (hit.distance < obstacleRange)
        //    {
        //        angle = Random.Range(-5, 5);
        //        transform.Rotate(0, angle, 0);
        //    }
        //    else
        //    {
        //        targetPosition = target.position;
        //        rb.velocity = (targetPosition - transform.position).normalized * speed;
        //        transform.LookAt(targetPosition);
        //    }
        //}

        if (health <= 0)
        {
            Destroy(gameObject);
            //gm.SetScore(gm.GetScore() + 1);
        }
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(3f);
        currentBullet = Instantiate(enemyBulletPrefab, Vector3.zero, Quaternion.identity);
    }

    public void Damage(int damage)
    {
        health -= damage;
    }
}
