using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Управление игроком и камерой
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runSpeed = 4f;
    [SerializeField] private float crouchSpeed = 1f;
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Look")]
    [SerializeField] private Transform playerCamera = null;
    [SerializeField] [Range(0.5f, 5f)]private float lookSensitivity = 2f;
    [SerializeField] [Range(0.0f, 0.1f)]private float smoothTime = 0.05f;

    [Header("Crouch")]
    [SerializeField] private float crouchHeight = 0.3f;
    [SerializeField] private float standingHeight = 0.6f;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;
    private Vector2 currentMouseDelta;
    private Vector2 currentMouseDeltaVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        controller.height = standingHeight;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleLook();
        HandleMovement();
        HandleCrouch();
    }

    private void HandleLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, smoothTime);

        // Вращение камеры по вертикали
        xRotation -= currentMouseDelta.y * lookSensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Вращение персонажа по горизонтали
        transform.Rotate(Vector3.up * currentMouseDelta.x * lookSensitivity);
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Определяем скорость
        float speed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) speed = runSpeed;
        if (Input.GetKey(KeyCode.LeftControl)) speed = crouchSpeed;

        // Прыжок и гравитация
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move((move * speed + velocity) * Time.deltaTime);
    }
    
    private void HandleCrouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
            controller.height = Mathf.Lerp(controller.height, crouchHeight, Time.deltaTime * 10f);
        else
            controller.height = Mathf.Lerp(controller.height, standingHeight, Time.deltaTime * 10f);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AIController enemy))
        {
            if (!enemy.IsHasKnife)
                return;
            StartSceneParam.IsWin = false;
            SceneManager.LoadScene("WinOrLoseScene");
        }
    }
}
