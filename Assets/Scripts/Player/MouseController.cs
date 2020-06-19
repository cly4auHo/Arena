using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private Transform playerBody;
    [Range(100, 500)]
    [SerializeField] private float mouseSensitivity = 150f;
    [Range(-180, 180)]
    [SerializeField] private float maxVert = 120f;
    [Range(-180, 180)]
    [SerializeField] private float minVert = -30f;
    private float mouseX;
    private float mouseY;
    private float xRotation;

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minVert, maxVert);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
