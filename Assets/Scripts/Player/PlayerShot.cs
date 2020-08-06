using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [SerializeField] private Reusable bulletPrefab;
    [SerializeField] private PoolManager poolManager;
    private Player player;
    private const string playerTag = "Player";
    private int size = 12;
    private float posX;
    private float posY;
    private bool aim;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).GetComponent<Player>();
        GoAim();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Shot();
        else if (Input.GetMouseButtonDown(1))
            player.Ultimate();
    }

    private void Shot()
    {
        Reusable currentBullet = poolManager.Instantiate(bulletPrefab, transform.position, transform.rotation);
        currentBullet.transform.position = transform.TransformPoint(Vector3.forward);
    }

    private void OnGUI()
    {
        if (aim)
        {
            posX = Camera.main.pixelWidth / 2 - size / 4;
            posY = Camera.main.pixelHeight / 2 - size / 2;
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
