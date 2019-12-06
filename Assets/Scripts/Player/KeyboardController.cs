using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    [SerializeField] private float speed = 2.5f;
    private CharacterController charController;
    private float gravity = -3f;

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

        if (!charController.isGrounded)
        {
            movement.y = gravity;
        }

        movement *= Time.deltaTime;

        charController.Move(movement);
    }
}
