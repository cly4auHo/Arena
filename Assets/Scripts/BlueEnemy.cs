﻿using UnityEngine;
using UnityEngine.AI;

public class BlueEnemy : Enemy
{
    private NavMeshAgent nav;
    private Transform player;
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
