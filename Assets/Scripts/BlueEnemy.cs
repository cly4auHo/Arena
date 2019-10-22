using UnityEngine;
using UnityEngine.AI;

public class BlueEnemy : MonoBehaviour
{
    private int health = 100;
    private Transform player;
    private NavMeshAgent nav;
    private const string playerTag = "Player";

    [SerializeField] private GameObject enemyBulletPrefab;
    private GameObject currentBullet;
    private float timeAttack = 5;
    private float timer = 0;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).transform;
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        nav.SetDestination(player.transform.position);

        if (Time.timeSinceLevelLoad - timeAttack > timer)
        {
            currentBullet = Instantiate(enemyBulletPrefab);
            currentBullet.transform.position = transform.TransformPoint(Vector3.forward);
            currentBullet.transform.rotation = transform.rotation;

            ChangeTimer();
        }
    }

    private void ChangeTimer()
    {
        timer = Time.timeSinceLevelLoad;
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public int GetHealth()
    {
        return health;
    }
}
