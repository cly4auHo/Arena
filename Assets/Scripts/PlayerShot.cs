using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    private Camera camera;
    private int size = 12;
    private float posX;
    private float posY;

    [SerializeField] private GameObject bulletPrefab;
    private GameObject currentBullet;

    private Player player;
    private GameManager gm;
    private const string RedEnemyTag = "RedEnemy";
    private const string BlueEnemyTag = "BlueEnemy";
    private int fullStrengt = 100;

    void Start()
    {
        player = FindObjectOfType<Player>();
        gm = FindObjectOfType<GameManager>();

        camera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Shot
        {
            currentBullet = Instantiate(bulletPrefab);
            currentBullet.transform.position = transform.TransformPoint(Vector3.forward);
            currentBullet.transform.rotation = transform.rotation;
        }
        else if (Input.GetMouseButtonDown(1)) // Ult
        {
            if (player.GetStrengt() >= fullStrengt)
            {
                player.SetStrengt(0);

                foreach (GameObject RedEnemies in GameObject.FindGameObjectsWithTag(RedEnemyTag))
                {
                    Destroy(RedEnemies);
                    gm.ScoreUp();
                }
                foreach (GameObject BlueEnemies in GameObject.FindGameObjectsWithTag(BlueEnemyTag))
                {
                    Destroy(BlueEnemies);
                    gm.ScoreUp();
                }
            }
        }
    }

    void OnGUI()
    {
        posX = camera.pixelWidth / 2 - size / 4;
        posY = camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
}
