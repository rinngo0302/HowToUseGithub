using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravity = -9.8f;

    [Header("Mouse Look Settings")]
    public float mouseSensitivity = 100f;
    public Transform cameraTransform; // Reference to the camera

    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    // SE
    private bool _isPlayMovingSE = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center
    }

    void Update()
    {
        // Handle player movement
        HandleMovement();
    }

    void HandleMovement()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force to keep grounded
        }

        // Get movement inputs (WASD or Arrow keys)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Create a movement vector based on input
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // SE System
        if (move != Vector3.zero && !_isPlayMovingSE)
        {
            _isPlayMovingSE = true;
            SE.Instance.Play(0, true);
            //Debug.Log("Play Moving SE!");
        }
        
        if (move == Vector3.zero && _isPlayMovingSE)
        {
            _isPlayMovingSE = false;
            SE.Instance.Stop(0);
            //Debug.Log("Stop Moving SE!");
        }

        // Move the character
        characterController.Move(move * moveSpeed * Time.deltaTime);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Handle jumping (only if the player is grounded)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity); // Jump based on the physics formula
        }

        // Apply gravity and move character
        characterController.Move(velocity * Time.deltaTime);
    }
}
