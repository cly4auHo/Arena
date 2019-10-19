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

    public void StrengthLess(int fade)
    {
        strength -= fade;
    }

    public int GetHealth()
    {
        return health;
    }
}
