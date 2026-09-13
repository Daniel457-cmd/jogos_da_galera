using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0.0f, 5.0f, -7.0f);
    public float smoothTime = 0.15f;
    public float lookHeight = 1.0f;
    public float rotationSpeed = 3.0f;
    public float minPitch = -30.0f;
    public float maxPitch = 70.0f;
    public bool lockCursor = true;

    private Vector3 currentVelocity;
    private float yaw;
    private float pitch;

    private void Start()
    {
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
        SetCursorLock();
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            SetCursorLock();
        }
    }

    private void SetCursorLock()
    {
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !lockCursor;
    }

    private void Update()
    {
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0.0f);
        Vector3 desiredPosition = target.position + rotation * offset;
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref currentVelocity,
            smoothTime
        );

        transform.LookAt(target.position + Vector3.up * lookHeight);
    }
}
