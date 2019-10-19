using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private RotationAxes axes = RotationAxes.MouseXAndY;
  
    [SerializeField] private float sensitivityHor = 3.0f;
    [SerializeField] private float sensitivityVert = 3.0f;
    private float minimumVert = -45.0f;
    private float maximumVert = 45.0f;

    private float rotationX;
    private float rotationY;
    private float delta;
  
    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
            body.freezeRotation = true;
    }

    void Update()
    {       
        if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            rotationX = Mathf.Clamp(rotationX, minimumVert, maximumVert);
            rotationY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        }
        else
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
