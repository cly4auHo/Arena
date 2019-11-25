using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private Transform playerBody;

    private float mouseSensitivity = 150f;
    private float mouseX;
    private float mouseY;
    private float xRotation = 0f;

    private float maxVert = 100f;
    private float minVert = -30f;

    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minVert, maxVert);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
