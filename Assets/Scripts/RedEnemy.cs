using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : MonoBehaviour
{   
    private int health = 50;
    [SerializeField] private float speed = 5f;


    void Start()
    {      
        health = 50;
    }

    void Update()
    {
      
      
    }

    public void Damage(int damage)
    {
        health -= damage;
    }

    public int GetHealth()
    {
        return health;
    }
}
