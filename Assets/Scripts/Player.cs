using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int health;
    private int strength;

    void Start()
    {
        health = 100;
        strength = 50;
    }

    void Update()
    {


    }
   
    public int GetHealth()
    {
        return health;
    }

    public void SetStrength(int strength)
    {
        this.strength = strength;
    }

    public int GetStrength()
    {
        return strength;
    }
}
