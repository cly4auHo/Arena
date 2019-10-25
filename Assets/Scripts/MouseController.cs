using UnityEngine;

public class MouseController : MonoBehaviour
{
    private Player player;
    [SerializeField] private RotationAxes axes = RotationAxes.MouseXAndY;

    [SerializeField] private float sensitivityHor = 3.0f;
    [SerializeField] private float sensitivityVert = 3.0f;
    private float minimumVert = -45.0f;
    private float maximumVert = 60.0f;

    private float rotationX;
    private float rotationY;
    private float delta;

    void Start()
    {
        player = FindObjectOfType<Player>();

        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
            body.freezeRotation = true;
    }

    void Update()
    {
        if (!player.IsPaused() && axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        }
        else if (!player.IsPaused() && axes == RotationAxes.MouseY)
        {
            rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            rotationX = Mathf.Clamp(rotationX, minimumVert, maximumVert);
            rotationY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        }
        else if (!player.IsPaused() && axes == RotationAxes.MouseXAndY)
        {
            rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            rotationX = Mathf.Clamp(rotationX, minimumVert, maximumVert);

            delta = Input.GetAxis("Mouse X") * sensitivityHor;
            rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        }
    }
}

public enum RotationAxes
{
    MouseXAndY,
    MouseX,
    MouseY
}
