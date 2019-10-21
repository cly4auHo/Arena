using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    private float timeOfSpawn = 5f;
    private float timer = 0;
    private float minTime = 2f;
    private float deltaTime = 0.5f;

    [SerializeField] private GameObject RedEnemyPrefab;
    [SerializeField] private GameObject BlueEnemyPrefab;
    private GameObject currentRedEnemy;
    private GameObject currentBlueEnemy;

    void Start()
    {
        timeOfSpawn = 5f;
        timer = 0;
    }

    //void Update()
    //{
    //    if (Time.timeSinceLevelLoad - timer > timeOfSpawn)
    //    {
    //        for (int i = 0; i < 4; i++)
    //        {
    //            currentRedEnemy = Instantiate(RedEnemyPrefab, Vector3.zero, Quaternion.identity);
    //        }
    //        currentBlueEnemy = Instantiate(BlueEnemyPrefab, Vector3.zero, Quaternion.identity);

    //        ChangeTime();
    //    }
    //}

    private void ChangeTime()
    {
        timer = Time.timeSinceLevelLoad;
        timeOfSpawn = Mathf.Max(timeOfSpawn - deltaTime, minTime);
    }
}
