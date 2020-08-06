using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BlueEnemy : Enemy
{
    [Header("Personal characteristics")]
    [SerializeField] private Reusable enemyBulletPrefab;
    [Range(1, 10)]
    [SerializeField] private float timeAttack = 5;
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
        Reusable currentBullet = poolManager.Instantiate(enemyBulletPrefab, transform.position, transform.rotation, transform);
        currentBullet.transform.position = transform.TransformPoint(Vector3.forward);
    }
}
