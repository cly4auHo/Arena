using UnityEngine;
using System.Collections;

public class PlayerShot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    private GameObject currentBullet;

    private Player player;
    private GameManager gm;
    private const string EnemyTag = "Enemy";
    private int fullStrengt = 100;

    private Camera firstCamera;
    private int size = 12;
    private float posX;
    private float posY;
    private bool aim = true;

    void Start()
    {
        firstCamera = GetComponentInChildren<Camera>();
        GoAim();

        gm = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shot();
        }
        else if (Input.GetMouseButtonDown(1) && player.GetStrengt() >= fullStrengt)
        {
            Ult();
        }
    }

    private void Shot()
    {
        currentBullet = Instantiate(bulletPrefab);
        currentBullet.transform.position = transform.TransformPoint(Vector3.forward);
        currentBullet.transform.rotation = transform.rotation;
    }

    private void Ult()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);

        if (enemies.Length == 0)
        {
            return;
        }

        player.SetStrengt(0);

        foreach (GameObject enemy in enemies)
        {
            gm.ScoreUp();
            Destroy(enemy);
        }
    }

    void OnGUI()
    {
        if (aim)
        {
            posX = firstCamera.pixelWidth / 2 - size / 4;
            posY = firstCamera.pixelHeight / 2 - size / 2;
            GUI.Label(new Rect(posX, posY, size, size), "*");
        }
    }

    public void StopAim()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        aim = false;
    }

    public void GoAim()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        aim = true;
    }
}
