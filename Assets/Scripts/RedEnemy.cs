﻿using UnityEngine;

public class RedEnemy : MonoBehaviour
{
    private int health = 50;
    private int damage = 15;
    [SerializeField] private float speed = 3.5f;
    private float yTop = 5f;

    private Vector3 myPosition;
    private Transform playerPosition;
    private Rigidbody rb;
    private Player player;
    private const string playerTag = "Player";

    private Transform currentPosition;
    private float timeOut = 5f;
    private float timer;
    private float jumpHight = 3;

    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag(playerTag).transform;
        rb = GetComponent<Rigidbody>();
        currentPosition = GetComponent<Transform>();

        timer = Time.timeSinceLevelLoad;
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad - timer < timeOut)
        {
            if (currentPosition.position.y != yTop)
            {

            }
        }
        else
        {
            myPosition = playerPosition.position;
            rb.velocity = (myPosition - transform.position).normalized * speed;
            transform.LookAt(myPosition);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            player = other.GetComponent<Player>();

            if (playerPosition)
            {
                player.SetHealth(Mathf.Min(player.GetHealth() - damage, 0));
            }

            Destroy(gameObject);
        }
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public int GetHealth()
    {
        return health;
    }
}
