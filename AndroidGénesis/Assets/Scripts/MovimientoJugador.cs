using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovimentoJugador : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 5f;
    public float crouchSpeed = 2.5f;
    public float jumpHeight = 1.5f;
    public float gravity = -19.62f;

    [Header("Cámara")]
    public Transform cameraTransform;
    public float mouseSensitivity = 2f;

    [Header("Agacharse")]
    public float standingHeight = 2.0f;
    public float crouchingHeight = 1.0f;

    private CharacterController controller;
    private Vector3 velocity;
    private float verticalRotation = 0f;
    private bool isCrouching = false;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Configuración inicial limpia del CharacterController
        controller.height = standingHeight;
        controller.center = new Vector3(0, standingHeight / 2f, 0);
    }

    void Update()
    {
        // 1. Detección de suelo real con Raycast (A prueba de fallos)
        // Lanza un rayo muy corto desde la base de la cápsula hacia abajo
        isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 0.25f);

        // 2. Control de la Cámara (Mouse)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // 3. Lógica de Agacharse
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;

            if (isCrouching)
            {
                controller.height = crouchingHeight;
                controller.center = new Vector3(0, crouchingHeight / 2f, 0);
                cameraTransform.localPosition = new Vector3(0, crouchingHeight * 0.8f, 0);
            }
            else
            {
                controller.height = standingHeight;
                controller.center = new Vector3(0, standingHeight / 2f, 0);
                cameraTransform.localPosition = new Vector3(0, standingHeight * 0.8f, 0);
            }
        }

        // 4. Movimiento Horizontal
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float currentSpeed = isCrouching ? crouchSpeed : walkSpeed;
        Vector3 move = (transform.right * x + transform.forward * z) * currentSpeed;

        controller.Move(move * Time.deltaTime);

        // 5. Salto y Gravedad
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}