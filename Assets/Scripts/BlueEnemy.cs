using UnityEngine;
using UnityEngine.AI;

public class BlueEnemy : Enemy
{
    [SerializeField] private GameObject enemyBulletPrefab;
    private GameObject currentBullet;

    private NavMeshAgent nav;
    private float timeAttack = 5;
    private float timer = 0;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        nav.SetDestination(playerPosition.transform.position);

        if (Time.timeSinceLevelLoad - timeAttack > timer)
        {
            Shot();
            ChangeTimer();
        }
    }

    void Shot()
    {
        currentBullet = Instantiate(enemyBulletPrefab);
        currentBullet.transform.position = transform.TransformPoint(Vector3.forward);
        currentBullet.transform.rotation = transform.rotation;
    }

    private void ChangeTimer()
    {
        timer = Time.timeSinceLevelLoad;
    }
}
