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
    [SerializeField] private GameObject RedEnemyPrefab;
    [SerializeField] private GameObject BlueEnemyPrefab;
    private Enemy[] enemies;
    private bool spawned;
    private Player player;
    private const string PlayerTag = "Player";

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<Player>();
        player.Die += StopSpawn;
        spawned = true;
        StartCoroutine(Creaate());
    }

    private IEnumerator Creaate()
    {
        while (spawned)
        {
            yield return new WaitForSeconds(timeOfSpawn);

            for (int i = 0; i < 4; i++)
                Instantiate(RedEnemyPrefab, RandomPosition(), Quaternion.identity);

            Instantiate(BlueEnemyPrefab, RandomPosition(), Quaternion.identity);
            timeOfSpawn = Mathf.Max(timeOfSpawn - deltaTime, minTime);
        }
    }

    private void StopSpawn()
    {
        spawned = false;
        enemies = FindObjectsOfType<Enemy>();

        if (enemies.Length != 0)
            foreach (Enemy enemy in enemies)
                Destroy(enemy);

        player.Die -= StopSpawn;
    }

    private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(xTop, xBot), yHight, Random.Range(zLeft, zRight));
    }

    public void SetSpawn(bool spawned)
    {
        this.spawned = spawned;
    }
}
