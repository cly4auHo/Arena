﻿using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    protected Player player;
    protected Vector3 playerPosition;
    protected const string playerTag = "Player";

    private GameManager gm;

    protected void Start()
    {
        gm = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
    }

    public void Damage(int damage, int strengtUp)
    {
        health -= damage;

        if (health <= 0)
        {
            Die(strengtUp);
        }
    }

    private void Die(int strengtUp)
    {
        gm.ScoreUp();
        player.StrengtUp(strengtUp);
        Destroy(gameObject);
    }

    public int GetHealth()
    {
        return health;
    }
}