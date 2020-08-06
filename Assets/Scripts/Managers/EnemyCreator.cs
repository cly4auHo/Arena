using UnityEngine;
using System.Collections;

public class EnemyCreator : MonoBehaviour
{
    [Header("Settings")]
    [Range(0, 5)]
    [SerializeField] private float timeOfSpawn = 5f;
    [Range(0, 3)]
    [SerializeField] private float minTime = 2f;
    [Range(0, 1)]
    [SerializeField] private float deltaTime = 0.25f;
    [Header("Zone where enemies will be instaiate")]
    [Range(0, 5)]
    [SerializeField] private float yHight = 2f;
    [Range(0, 5)]
    [SerializeField] private float xTop = 2.5f;
    [Range(-5, 0)]
    [SerializeField] private float xBot = -2.5f;
    [Range(-5, 0)]
    [SerializeField] private float zLeft = -2.5f;
    [Range(0, 5)]
    [SerializeField] private float zRight = 2.5f;
    [Header("Enemies")]
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private Reusable RedEnemyPrefab;
    [SerializeField] private Reusable BlueEnemyPrefab;
    [Range(1, 10)]
    [SerializeField] private int amountOfRed = 4;
    private bool spawned;
    private Player player;
    private const string PlayerTag = "Player";
    private const string pauseManagerTag = "PauseManager";

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<Player>();
        player.Die += StopSpawn;
        GameObject.FindGameObjectWithTag(pauseManagerTag).GetComponent<Pause>().RestartGame += StartSpawn;
        spawned = true;
        StartCoroutine(Creaate());
    }

    public void SetSpawn(bool spawned)
    {
        this.spawned = spawned;
    }

    private IEnumerator Creaate()
    {
        while (spawned)
        {
            yield return new WaitForSeconds(timeOfSpawn);

            for (int i = 0; i < amountOfRed; i++)
                poolManager.Instantiate(RedEnemyPrefab, RandomPosition(), Quaternion.identity);

            poolManager.Instantiate(BlueEnemyPrefab, RandomPosition(), Quaternion.identity);
            timeOfSpawn = Mathf.Max(timeOfSpawn - deltaTime, minTime);
        }
    }

    private void StopSpawn()
    {
        spawned = false;
    }

    private void StartSpawn()
    {

    }

    private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(xTop, xBot), yHight, Random.Range(zLeft, zRight));
    }
}
