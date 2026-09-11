using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class movement : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 7.0f;
    public LayerMask groundMask = ~0;
    public float groundCheckDistance = 0.1f;

    private Rigidbody playerRigidbody;
    private Collider playerCollider;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        playerRigidbody.freezeRotation = true;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Vector3 velocity = playerRigidbody.linearVelocity;
            velocity.y = jumpForce;
            playerRigidbody.linearVelocity = velocity;
        }
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput);

        if (movement.sqrMagnitude > 1.0f)
        {
            movement.Normalize();
        }

        Vector3 velocity = playerRigidbody.linearVelocity;
        velocity.x = movement.x * speed;
        velocity.z = movement.z * speed;
        playerRigidbody.linearVelocity = velocity;
    }

    private bool IsGrounded()
    {
        Bounds bounds = playerCollider.bounds;
        Vector3 origin = bounds.center + Vector3.up * 0.05f;
        float distance = bounds.extents.y + groundCheckDistance;

        return Physics.Raycast(origin, Vector3.down, distance, groundMask, QueryTriggerInteraction.Ignore);
    }   
}
