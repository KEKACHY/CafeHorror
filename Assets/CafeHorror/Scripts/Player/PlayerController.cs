using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Управление игроком и камерой
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float stepIntervalWalk = 0.5f;
    [SerializeField] private float stepIntervalRun = 0.35f;
    [SerializeField] private float stepIntervalCrouch = 0.8f;
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

    [SerializeField] private AudioSource FootStepAudio;
    
    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;
    private Vector2 currentMouseDelta;
    private Vector2 currentMouseDeltaVelocity;
    private float stepTimer;
    [HideInInspector] public bool forceLook = false;
    [HideInInspector] public Transform targetPoint;

    [SerializeField] private Dialogue[] dialogue;
    

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        controller.height = standingHeight;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        DialogueManager.Instance.StartDialogue(dialogue[0]);
    }

    private void Update()
    {
        if (!forceLook)
            HandleLook();

        HandleMovement();
        HandleCrouch();

        if (forceLook)
            LookAtSmooth(targetPoint, 4f);
    }

    private void HandleLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, smoothTime);

        xRotation -= currentMouseDelta.y * lookSensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * currentMouseDelta.x * lookSensitivity);
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        float speed = walkSpeed;
        float stepInterval = stepIntervalWalk;

        if (Input.GetKey(KeyCode.LeftShift)) {
            speed = runSpeed;
            stepInterval = stepIntervalRun;
        }

        if (Input.GetKey(KeyCode.LeftControl)) {
            speed = crouchSpeed;
            stepInterval = stepIntervalCrouch;
        }

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;

        controller.Move((move * speed + velocity) * Time.deltaTime);

        HandleFootsteps(move, stepInterval);
    }
    
    private void HandleCrouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            controller.height = Mathf.Lerp(controller.height, crouchHeight, Time.deltaTime * 10f); FootStepAudio.volume = 0.05f;
        }
        else
            controller.height = Mathf.Lerp(controller.height, standingHeight, Time.deltaTime * 10f); FootStepAudio.volume = 0.1f;
    }

    private void HandleFootsteps(Vector3 move, float interval)
    {
        if (!controller.isGrounded) return;

        stepTimer -= Time.deltaTime;

        if (stepTimer <= 0f)
        {
            if (move.magnitude < 0.1f)
            {
                FootStepAudio.Stop();
                stepTimer = 0f;
                return;
            }
            FootStepAudio.pitch = Random.Range(0.9f, 1.1f);
            FootStepAudio.Play();

            stepTimer = interval; 
        }
    }

    public void LookAtSmooth(Transform target, float speed)
    {
        Vector3 dir = target.position - playerCamera.position;

        Vector3 flat = dir; flat.y = 0;
        Quaternion bodyRot = Quaternion.LookRotation(flat);
        transform.rotation = Quaternion.Lerp(transform.rotation, bodyRot, Time.deltaTime * speed);

        Quaternion camRot = Quaternion.LookRotation(dir);
        playerCamera.rotation = Quaternion.Lerp(playerCamera.rotation, camRot, Time.deltaTime * speed);
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
