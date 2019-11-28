using UnityEngine;
using System.Collections;

public class EnemyCreator : MonoBehaviour
{
    private float yHight = 2f; //zone where enemies will be instaiate
    private float xTop = 2.5f;
    private float xBot = -2.5f;
    private float zLeft = -2.5f;
    private float zRight = 2.5f;

    private bool spawned;
    [SerializeField] private GameObject RedEnemyPrefab;
    [SerializeField] private GameObject BlueEnemyPrefab;

    private float timeOfSpawn = 5f;
    private float minTime = 2f;
    private float deltaTime = 0.5f;

    private void Start()
    {
        spawned = true;
        StartCoroutine(Creaate());
    }

    private IEnumerator Creaate()
    {
        while (spawned)
        {
            yield return new WaitForSeconds(timeOfSpawn);

            for (int i = 0; i < 4; i++)
            {
                Instantiate(RedEnemyPrefab, RandomPosition(), Quaternion.identity);
            }

            Instantiate(BlueEnemyPrefab, RandomPosition(), Quaternion.identity);

            timeOfSpawn = Mathf.Max(timeOfSpawn - deltaTime, minTime);
        }
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
