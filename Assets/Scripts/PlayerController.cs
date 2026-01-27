using System;
using System.Collections;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Camera Rotation
    private float mouseSensitivity = 2f;
    private float verticalRotation = 0f;
    private Transform cameraTransform;

    // Ground Movement
    private Rigidbody rb;
    private float MoveSpeed = 10f;
    private float moveHorizontal;
    private float moveForward;

    private float playerYPosition;

    // Jumping
    private float jumpForce = 10f;
    private float fallMultiplier = 2.5f; // Multiplies gravity when falling down
    private float ascendMultiplier = 2f; // Multiplies gravity for ascending to peak of jump
    private bool isGrounded = true;
    public LayerMask groundLayer = ~0; // default to all layers; set in Inspector if needed
    private float groundCheckTimer = 0f;
    private float groundCheckDelay = 0.3f;
    private float playerHeight;
    private float raycastDistance;

    // texts
    public GameObject winText;
    public GameObject loseText;

    private float endScreenDelay = 2f; // seconds to show win/lose text before continuing

    // Input control
    private bool acceptInput = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        cameraTransform = Camera.main.transform;

        // Set the raycast to be slightly beneath the player's feet
        playerHeight = GetComponent<CapsuleCollider>().height * transform.localScale.y;
        raycastDistance = (playerHeight / 2) + 0.2f;

        // Hides the mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (winText != null) winText.SetActive(false);
        if (loseText != null) loseText.SetActive(false);
    }

    void Update()
    {
        if (acceptInput)
        {
            moveHorizontal = Input.GetAxisRaw("Horizontal");
            moveForward = Input.GetAxisRaw("Vertical");

            RotateCamera();

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                Jump();
            }
        }
        else
        {
            // ensure no residual movement input when input is disabled
            moveHorizontal = 0f;
            moveForward = 0f;
        }

        // Checking when we're on the ground and keeping track of our ground check delay
        if (!isGrounded && groundCheckTimer <= 0f)
        {
            Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
            isGrounded = Physics.Raycast(rayOrigin, Vector3.down, raycastDistance, groundLayer);
        }
        else
        {
            groundCheckTimer -= Time.deltaTime;
        }

    }

    void FixedUpdate()
    {
        MovePlayer();
        ApplyJumpPhysics();

        if (rb.position.y < -10f)
        {
            StartCoroutine(ShowThenContinue(loseText, null));
        }
    }

    void MovePlayer()
    {
        Vector3 movement = (transform.right * moveHorizontal + transform.forward * moveForward).normalized;
        Vector3 targetVelocity = movement * MoveSpeed;

        // Apply movement to the Rigidbody
        Vector3 velocity = rb.velocity;
        velocity.x = targetVelocity.x;
        velocity.z = targetVelocity.z;
        rb.velocity = velocity;

        // If we aren't moving and are on the ground, stop velocity so we don't slide
        if (isGrounded && moveHorizontal == 0 && moveForward == 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    void RotateCamera()
    {
        float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, horizontalRotation, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    void Jump()
    {
        isGrounded = false;
        groundCheckTimer = groundCheckDelay;
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z); // Initial burst for the jump
    }

    void ApplyJumpPhysics()
    {
        if (rb.velocity.y < 0)
        {
            // Falling: Apply fall multiplier to make descent faster
            rb.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        } // Rising
        else if (rb.velocity.y > 0)
        {
            // Rising: Change multiplier to make player reach peak of jump faster
            rb.velocity += Vector3.up * Physics.gravity.y * ascendMultiplier * Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("damage"))
        {
            // show lose text then restart (default continuation)
            StartCoroutine(ShowThenContinue(loseText, null));
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            // show win text then go to next level (custom continuation)
            StartCoroutine(ShowThenContinue(winText, NextLevel));
        }
    }

    // Shows the provided UI element, waits, then executes continuation if provided.
    // If continuation is null, defaults to reloading the current scene.
    private IEnumerator ShowThenContinue(GameObject text, Action continuation)
    {
        // stop processing further player inputs immediately
        DisableInput();

        if (text != null)
            text.SetActive(true);

        yield return new WaitForSeconds(endScreenDelay);

        if (continuation != null)
            continuation();
        else
            RestartScene();
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void NextLevel()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        if (next < SceneManager.sceneCountInBuildSettings - 1)
            SceneManager.LoadScene(next);
        else
            SceneManager.LoadScene(0); // go back to main menu if no more levels
    }

    // Public API to disable/enable player input handling (movement, camera, jump)
    public void DisableInput()
    {
        acceptInput = false;
        moveHorizontal = 0f;
        moveForward = 0f;
    }

    public void EnableInput()
    {
        acceptInput = true;
    }
}
