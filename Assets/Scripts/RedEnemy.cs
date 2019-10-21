using UnityEngine.AI;
using UnityEngine;

public class RedEnemy : MonoBehaviour
{
    private int health = 50;
    private int damage = 15;
    private Transform playerPosition;
    private NavMeshAgent nav;
    private Player player;
    private const string playerTag = "Player";

    private float timeOut = 5;


    void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag(playerTag).transform;
        nav = GetComponent<NavMeshAgent>();
    }
  
    void Update()
    {
        if (Time.timeSinceLevelLoad < timeOut)
        {
            nav.SetDestination(playerPosition.position);
        }
        else
        {

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            player = other.GetComponent<Player>();

            if (player)
            {
                player.SetStrengt(Mathf.Min(player.GetHealth() - damage, 0));
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
