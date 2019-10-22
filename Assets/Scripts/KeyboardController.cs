using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    private CharacterController charController;

    [SerializeField] private float speed = 2.5f;
    private float gravity = -9.8f;
  
    private float deltaX;
    private float deltaZ;
    private Vector3 movement;

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        deltaX = Input.GetAxis("Horizontal") * speed;
        deltaZ = Input.GetAxis("Vertical") * speed;
        movement = new Vector3(deltaX, 0, deltaZ);

        movement = Vector3.ClampMagnitude(movement, speed);
        movement = transform.TransformDirection(movement);
        movement.y = gravity;
        movement *= Time.deltaTime;

        charController.Move(movement);
    }
}
