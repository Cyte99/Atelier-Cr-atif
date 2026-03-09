using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Mouvement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 7.0f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravityValue = -9.81f;

    [Header("References")]
    public CharacterController controller;
    [SerializeField] private Transform headPos;

    [Header("Input Actions")]
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public InputActionReference interactAction;

    [Header("Interact Settings")]
    [SerializeField] private Transform interactOrigin;     
    [SerializeField] private float interactDistance = 2.5f;
    [SerializeField] private LayerMask interactMask = ~0;  

    [Header("Look Settings")]
    [SerializeField] private float mouseSensitivity = 0.08f; 
    [SerializeField] private float upDownRange = 80f;
    [SerializeField] private float mouseDeadzone = 0.02f; 

    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private float yaw;
    private float pitch;

    private void Awake()
    {
        if (controller == null)
            controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        if (moveAction != null && moveAction.action != null) moveAction.action.Enable();
        else Debug.LogError("Player_Mouvement: moveAction is not assigned (or action is null).");

        if (jumpAction != null && jumpAction.action != null) jumpAction.action.Enable();
        else Debug.LogError("Player_Mouvement: jumpAction is not assigned (or action is null).");

        if (interactAction != null && interactAction.action != null) interactAction.action.Enable();
        else Debug.LogWarning("Player_Mouvement: interactAction is not assigned yet.");
    }

    private void OnDisable()
    {
        if (moveAction != null && moveAction.action != null) moveAction.action.Disable();
        if (jumpAction != null && jumpAction.action != null) jumpAction.action.Disable();
        if (interactAction != null && interactAction.action != null) interactAction.action.Disable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        yaw = transform.localEulerAngles.y;
        pitch = headPos.localEulerAngles.x;

        if (pitch > 180f) pitch -= 360f;
    }

    void Update()
    {
        if (controller == null)
        {
            Debug.LogError("Player_Mouvement: CharacterController is NOT assigned.");
            return;
        }

        if (headPos == null)
        {
            Debug.LogError("Player_Mouvement: headPos is NOT assigned.");
            return;
        }

        if (moveAction == null || moveAction.action == null)
        {
            Debug.LogError("Player_Mouvement: moveAction is NOT assigned (or action is null).");
            return;
        }

        if (jumpAction == null || jumpAction.action == null)
        {
            Debug.LogError("Player_Mouvement: jumpAction is NOT assigned (or action is null).");
            return;
        }

        bool hasInteract = interactAction != null && interactAction.action != null;

        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0f)
            playerVelocity.y = -2f;

        if (groundedPlayer && jumpAction.action.WasPressedThisFrame())
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);

        playerVelocity.y += gravityValue * Time.deltaTime;

        HandleMovement();
        HandleRotation();

        if (hasInteract && interactAction.action.WasPressedThisFrame())
            TryInteract();
    }

    private void HandleMovement()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();

        Vector3 move = new Vector3(input.x, 0f, input.y);
        move = transform.TransformDirection(move) * playerSpeed;

        Vector3 finalMove = move + Vector3.up * playerVelocity.y;
        controller.Move(finalMove * Time.deltaTime);
    }

    private void HandleRotation()
    {
        Vector2 delta = Mouse.current != null ? Mouse.current.delta.ReadValue() : Vector2.zero;

        if (Mathf.Abs(delta.x) < mouseDeadzone) delta.x = 0f;
        if (Mathf.Abs(delta.y) < mouseDeadzone) delta.y = 0f;

        yaw += delta.x * mouseSensitivity;
        pitch -= delta.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, -upDownRange, upDownRange);

        transform.localRotation = Quaternion.Euler(0f, yaw, 0f);
        headPos.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    private void TryInteract()
    {
        Transform origin = interactOrigin != null ? interactOrigin : headPos;
        if (origin == null) return;

        if (Physics.Raycast(origin.position, origin.forward, out RaycastHit hit,
                interactDistance, interactMask, QueryTriggerInteraction.Ignore))
        {
            IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact(this);
            }
        }
    }
}