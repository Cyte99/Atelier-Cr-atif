using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Mouvement : MonoBehaviour
{
    [SerializeField]private float playerSpeed = 5.0f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravityValue = -9.81f;

    public CharacterController controller;
    private Vector3 playerVelocity;
    [SerializeField] private bool groundedPlayer;

    [Header("Input Actions")]
    public InputActionReference moveAction;
    public InputActionReference jumpAction;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform headPos;
    [SerializeField] private float mouseSensitivity = 3f;
    [SerializeField] private float verticalRotation;
    [SerializeField] private string MouseXInput = "Mouse X";
    [SerializeField] private string MouseYInput = "Mouse Y";
    [SerializeField] private float upDownRange = 80f;

    private void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();

        groundedPlayer = controller.isGrounded;

        if (groundedPlayer)
        {
             //Slight downward velocity to keep grounded stable
            if (playerVelocity.y < -2f)
                playerVelocity.y = -2f;
        }

        //// Read input
        //Vector2 input = moveAction.action.ReadValue<Vector2>();
        //Vector3 move = new Vector3(input.x, 0, input.y);
        //move = Vector3.ClampMagnitude(move, 1f);

        //if (move != Vector3.zero)
        //    transform.forward = move;

        //// Jump using WasPressedThisFrame()
        if (groundedPlayer && jumpAction.action.WasPressedThisFrame())
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
        }

        //// Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;

        //// Move
        //Vector3 finalMove = move * playerSpeed + Vector3.up * playerVelocity.y;
        //controller.Move(finalMove * Time.deltaTime);
    }

    private void HandleMovement()
    {
    
        float verticalSpeed = Input.GetAxis("Vertical");
        float horizontalSpeed = Input.GetAxis("Horizontal");
        Vector3 speed = new Vector3(horizontalSpeed, 0, verticalSpeed);
        speed = transform.rotation * speed * playerSpeed;

        controller.SimpleMove(speed);


    }

    void HandleRotation()
    {

        //float mouseXRotation = Input.GetAxis(MouseXInput) * mouseSensitivity;
        //transform.Rotate(0, mouseXRotation, 0);

        //verticalRotation -= Input.GetAxis(MouseYInput) * mouseSensitivity;
        //verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        //mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        float mouseXRotation = Input.GetAxis(MouseXInput) * mouseSensitivity;
        float mouseYRotation = Input.GetAxis(MouseYInput) * mouseSensitivity;

        transform.Rotate(0, mouseXRotation, 0);

        verticalRotation -= mouseYRotation;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);

        Vector3 pivotEuler=headPos.eulerAngles;
        headPos.eulerAngles = new Vector3(verticalRotation, pivotEuler.y, 0f);

        transform.eulerAngles = new Vector3(0f,headPos.eulerAngles.y, 0f);
    }
    


}

