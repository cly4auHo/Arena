using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BlueEnemy : Enemy
{
    [Header("Personal characteristics")]
    [SerializeField] private GameObject enemyBulletPrefab;
    private GameObject currentBullet;

    [SerializeField] private float timeAttack = 5;
    private NavMeshAgent nav;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();

        StartCoroutine(Shot());
    }

    void Update()
    {
        playerPosition = player.transform.position;
        nav.SetDestination(playerPosition);
    }

    private IEnumerator Shot()
    {
        yield return new WaitForSeconds(timeAttack);

        currentBullet = Instantiate(enemyBulletPrefab);
        currentBullet.transform.position = transform.TransformPoint(Vector3.forward);
        currentBullet.transform.rotation = transform.rotation;
    }
}
