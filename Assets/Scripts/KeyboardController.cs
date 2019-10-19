using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    private CharacterController charController;

    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float jumpSpeed = 7.5f;
    private float gravity = -9.8f;
    private float terminalVelocity = -10.0f;
    private float minFall = -1.5f;
    private float vertSpeed;

    private float deltaX;
    private float deltaZ;
    private Vector3 movement;

    void Start()
    {
        vertSpeed = minFall;
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        deltaX = Input.GetAxis("Horizontal") * speed;
        deltaZ = Input.GetAxis("Vertical") * speed;

        movement = new Vector3(deltaX, 0, deltaZ);

        movement = Vector3.ClampMagnitude(movement, speed);
        movement = transform.TransformDirection(movement);

        if (charController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                vertSpeed = jumpSpeed;
            }
            else
            {
                vertSpeed = minFall;
            }
        }
        else
        {
            vertSpeed += gravity * 5 * Time.deltaTime;
            if (vertSpeed < terminalVelocity)
            {
                vertSpeed = terminalVelocity;
            }
        }
        movement.y = vertSpeed;
        movement *= Time.deltaTime;
        charController.Move(movement);
    }
}
