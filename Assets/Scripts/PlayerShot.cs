using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    private GameObject currentBullet;

    private Player player;
    private const string RedEnemyTag = "RedEnemy";
    private const string BlueEnemyTag = "BlueEnemy";
    private int fullStrengt = 100;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Shot
        {
            currentBullet = Instantiate(bulletPrefab);
            currentBullet.transform.position = transform.TransformPoint(Vector3.forward);
            currentBullet.transform.rotation = transform.rotation;
        }
        else if (Input.GetMouseButtonDown(1) && player.GetStrengt() >= fullStrengt) // Ult
        {
            player.SetStrengt(0);

            foreach (GameObject RedEnemies in GameObject.FindGameObjectsWithTag(RedEnemyTag))
            {
                Destroy(RedEnemies);
                player.ScoreUp();
            }
            foreach (GameObject BlueEnemies in GameObject.FindGameObjectsWithTag(BlueEnemyTag))
            {
                Destroy(BlueEnemies);
                player.ScoreUp();
            }
        }
    }
}
