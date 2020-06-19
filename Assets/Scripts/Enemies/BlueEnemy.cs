using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BlueEnemy : Enemy
{
    [Header("Personal characteristics")]
    [SerializeField] private GameObject enemyBulletPrefab;
    [Range(1, 10)]
    [SerializeField] private float timeAttack = 5;
    private GameObject currentBullet;
    private NavMeshAgent nav;

    private new void Start()
    {
        base.Start();
        nav = GetComponent<NavMeshAgent>();
        StartCoroutine(Shot());
    }

    private new void Update()
    {
        base.Update();
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
