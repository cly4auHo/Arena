using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : MonoBehaviour
{
    private int health;


    void Start()
    {

        health = 50;
    }

    void Update()
    {

        if (health <= 0)
        {
            Destroy(gameObject);
            //gm.SetScore(gm.GetScore() + 1);
        }
    }

    public void Damage(int damage)
    {
        health -= damage;
    }
}
