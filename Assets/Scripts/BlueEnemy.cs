using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BlueEnemy : Enemy
{
    [SerializeField] private GameObject enemyBulletPrefab;
    private GameObject currentBullet;

    private NavMeshAgent nav;
    private float timeAttack = 5;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();

        StartCoroutine(Shot());
    }

    void Update()
    {
        nav.SetDestination(playerPosition.transform.position);
    }

    private IEnumerator Shot()
    {
        yield return new WaitForSeconds(timeAttack);

        currentBullet = Instantiate(enemyBulletPrefab);
        currentBullet.transform.position = transform.TransformPoint(Vector3.forward);
        currentBullet.transform.rotation = transform.rotation;
    }
}
