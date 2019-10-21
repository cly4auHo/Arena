using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    private float timeOfSpawn = 5f;
    private float minTime = 2f;
    private float deltaTime = 0.5f;

    [SerializeField] private GameObject RedEnemyPrefab;
    [SerializeField] private GameObject BlueEnemyPrefab;
    private GameObject currentRedEnemy;
    private GameObject currentBlueEnemy;

    void Start()
    {
        timeOfSpawn = 5f;
    }


    void Update()
    {
        StartCoroutine(Create());


    }

    private IEnumerator Create()
    {
        yield return new WaitForSeconds(timeOfSpawn);
        currentRedEnemy = Instantiate(RedEnemyPrefab, Vector3.zero, Quaternion.identity);
        currentBlueEnemy = Instantiate(BlueEnemyPrefab, Vector3.zero, Quaternion.identity);

        timeOfSpawn = Mathf.Max(timeOfSpawn - deltaTime, minTime);
    }
}
