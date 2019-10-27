using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    private float timeOfSpawn;
    private float timer;
    private float minTime = 2f;
    private float deltaTime = 0.5f;

    [SerializeField] private GameObject RedEnemyPrefab;
    [SerializeField] private GameObject BlueEnemyPrefab;

    private float yHight = 3f; //zone where enemies will be instaiate
    private float xTop = 2.5f;
    private float xBot = -2.5f;
    private float zLeft = -2.5f;
    private float zRight = 2.5f;

    void Start()
    {
        timeOfSpawn = 5f;
        timer = 0;
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad - timer > timeOfSpawn)
        {
            for (int i = 0; i < 4; i++)
            {
                Instantiate(RedEnemyPrefab, RandomPosition(), Quaternion.identity);
            }

            Instantiate(BlueEnemyPrefab, RandomPosition(), Quaternion.identity);

            ChangeTime();
        }
    }

    Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(xTop, xBot), yHight, Random.Range(zLeft, zRight));
    }

    private void ChangeTime()
    {
        timer = Time.timeSinceLevelLoad;
        timeOfSpawn = Mathf.Max(timeOfSpawn - deltaTime, minTime);
    }
}
