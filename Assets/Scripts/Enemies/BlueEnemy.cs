using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BlueEnemy : Enemy
{
    [Header("Personal characteristics")]
    [SerializeField] private GameObject enemyBulletPrefab;
    private GameObject currentBullet;

    [Range(1f, 5f)]
    [SerializeField] private float timeAttack = 5;
    private NavMeshAgent nav;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        StartCoroutine(Shot());
    }

    private void Update()
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
