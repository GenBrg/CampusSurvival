using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController characterController;

    public Transform groundCheck;
    public LayerMask groundLayerMask;
    public float groundCheckDistance = 0.2f;
    public float crouchHeightModifier = 0.4f;

    public float jumpHeight = 0.8f;

    public bool isGrounded;
    public bool isSprint;
    public bool isCrouch;

    public float sprintForwardMaxSpeedModifier = 1.5f;
    public float crouchMaxSpeedModifier = 0.5f;
    public float inAirMoveForceModifier = 0.1f;

    public float baseMoveForce = 40.0f;
    public float fraction = 20.0f;

    public float baseMaxSpeed = 5.0f;
    public float baseLateralSpeed = 5.0f;

    private Vector3 velocity;
    private InputManager input;

    private void Start()
    {
        input = FindObjectOfType<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Apply gravity
        velocity.y += -Constants.Gravity * Time.deltaTime;

        // Check Grouned
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundLayerMask);

        if (isGrounded)
        {
            velocity.y = -2f;

            if (input.Jump)
            {
                velocity.y = Mathf.Sqrt(2.0f * Constants.Gravity * jumpHeight);

                // Avoid immediate ground check
                characterController.Move(Vector3.up * groundCheckDistance);
            }
        }

        //// Update horizontal movement
        float horizontalInput = input.HorizontalInput;
        float verticalInput = input.VerticalInput;

        // Check stance
        isCrouch = input.Crouch;
        isSprint = isCrouch ? false : input.Sprint && 
            verticalInput > 0.0f &&
            Vector3.Dot(velocity, transform.forward) > 0.0f;

        // Velocity adjust
        float maxSpeed = baseMaxSpeed;
        float maxLateralSpeed = baseLateralSpeed;
        float moveForce = baseMoveForce;

        if (isCrouch)
        {
            maxSpeed *= crouchMaxSpeedModifier;
            maxLateralSpeed *= crouchMaxSpeedModifier;
        }
        else if (isSprint)
        {
            maxSpeed *= sprintForwardMaxSpeedModifier;
        }

        Vector3 scale = transform.localScale;
        scale.y = isCrouch ? crouchHeightModifier : 1.0f;
        transform.localScale = scale;

        if (!isGrounded)
        {
            moveForce *= inAirMoveForceModifier;
        }

        // F * dt / m 
        Vector3 velocityDelta = moveForce * Time.deltaTime * (horizontalInput * transform.right + verticalInput * transform.forward);
        velocity += velocityDelta;
        
        Vector2 horizontalMovement = new Vector2(velocity.x, velocity.z);
        float horizontalSpeed = horizontalMovement.magnitude;

        // Apply fraction
        if (isGrounded)
        {
            horizontalSpeed = Mathf.Max(0.0f, horizontalSpeed - fraction * Time.deltaTime);
            horizontalMovement = horizontalMovement.normalized * horizontalSpeed;
        }

        // Clamp horizontal speed
        Vector2 forward = new Vector2(transform.forward.x, transform.forward.z);
        Vector2 right = new Vector2(transform.right.x, transform.right.z);
        float forwardSpeed = Vector2.Dot(horizontalMovement, forward);
        float lateralSpeed = Mathf.Clamp(
            Vector2.Dot(horizontalMovement, right),
            -maxLateralSpeed, maxLateralSpeed);
        horizontalMovement = forwardSpeed * forward + lateralSpeed * right;

        float clampModifier = Mathf.Min(1.0f, maxSpeed / horizontalMovement.magnitude);
        horizontalMovement *= clampModifier;

        velocity.x = horizontalMovement.x;
        velocity.z = horizontalMovement.y;

        characterController.Move(velocity * Time.deltaTime);
    }
}
