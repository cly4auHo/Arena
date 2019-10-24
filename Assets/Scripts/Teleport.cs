using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private Player player;
    private EnemyBullet enemyBullet;

    void Start()
    {
        player = FindObjectOfType<Player>();
        enemyBullet = FindObjectOfType<EnemyBullet>();
    }

    void OnTriggerEnter(Collider other)
    {


        enemyBullet.AfterTeleport(NewPosition());
    }

    Vector3 NewPosition()
    {

        return Vector3.zero;
    }
}
