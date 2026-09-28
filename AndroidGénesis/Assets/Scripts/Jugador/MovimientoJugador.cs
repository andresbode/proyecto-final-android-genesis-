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

        // Mantenemos tu configuración por defecto
        controller.height = standingHeight;
        controller.center = Vector3.zero;

        // Posición inicial de la cámara a la altura de los ojos (dentro de la cápsula)
        if (cameraTransform != null)
        {
            cameraTransform.localPosition = new Vector3(0, 0.6f, 0);
        }
    }

    void Update()
    {
        // 1. Raycast desde la base real (Suelo en Y = -height/2)
        float medioAlto = controller.height / 2f;
        Vector3 baseDeLosPies = transform.position + controller.center - new Vector3(0, medioAlto - 0.1f, 0);
        isGrounded = Physics.Raycast(baseDeLosPies, Vector3.down, 0.25f);

        // 2. Rotación de Cámara y Cuerpo
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        if (cameraTransform != null)
        {
            cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }

        // 3. Agacharse
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;

            if (isCrouching)
            {
                controller.height = crouchingHeight;
                controller.center = Vector3.zero;
                if (cameraTransform != null) cameraTransform.localPosition = new Vector3(0, 0.2f, 0);
            }
            else
            {
                controller.height = standingHeight;
                controller.center = Vector3.zero;
                if (cameraTransform != null) cameraTransform.localPosition = new Vector3(0, 0.6f, 0);
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