using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    private Camera camera;
    private int size = 12;
    private float posX;
    private float posY;

    [SerializeField] private GameObject bulletPrefab;
    private GameObject currentBullet;

    private BlueEnemy blueEnemy;
    private RedEnemy redEnemy;

    void Start()
    {
        camera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentBullet = Instantiate(bulletPrefab);
            currentBullet.transform.position = transform.TransformPoint(Vector3.forward);
            currentBullet.transform.rotation = transform.rotation;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Ulty();
        }
    }

    void Ulty()
    {
        //blueEnemy = FindObjectsOfTypeAll(typeof(<BlueEnemy>));
    }

    void OnGUI()
    {
        posX = camera.pixelWidth / 2 - size / 4;
        posY = camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
}
