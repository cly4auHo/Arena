using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    private Player player;
    private float timeOfSpawn = 5f;
    private float timer = 0;
    private float minTime = 2f;
    private float deltaTime = 0.5f;

    [SerializeField] private GameObject RedEnemyPrefab;
    [SerializeField] private GameObject BlueEnemyPrefab;
    private GameObject CurrentRedEnemy;
    private GameObject CurrentBlueEnemy;

    private Vector3 EnemyPosition;
    private float yHightBlue = 1.25f;
    private float yHightRed = 1.1f;
    private float xTop = 2.5f;
    private float xBot = -2.5f;
    private float zLeft = -2.5f;
    private float zRight = 2.5f;

    void Start()
    {
        player = FindObjectOfType<Player>();
        timeOfSpawn = 5f;
        timer = 0;
    }

    void Update()
    {
        if (!player.IsPaused() && player.GetHealth() >= 0 && Time.timeSinceLevelLoad - timer > timeOfSpawn)
        {
            for (int i = 0; i < 4; i++)
            {
                CurrentRedEnemy = Instantiate(RedEnemyPrefab, RandomPositionRed(), Quaternion.identity);
            }
            CurrentBlueEnemy = Instantiate(BlueEnemyPrefab, RandomPositionBlue(), Quaternion.identity);

            ChangeTime();
        }
    }

    Vector3 RandomPositionRed()
    {
        return new Vector3(Random.Range(xTop, xBot), yHightRed, Random.Range(zLeft, zRight));
    }

    Vector3 RandomPositionBlue()
    {
        return new Vector3(Random.Range(xTop, xBot), yHightBlue, Random.Range(zLeft, zRight));
    }

    private void ChangeTime()
    {
        timer = Time.timeSinceLevelLoad;
        timeOfSpawn = Mathf.Max(timeOfSpawn - deltaTime, minTime);
    }
}
