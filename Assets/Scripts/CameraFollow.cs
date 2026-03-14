using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public Vector3 offset;

    public Vector2 minLimit;
    public Vector2 maxLimit;

    private float camHeight;
    private float camWidth;

    void Start()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        float clampedX = Mathf.Clamp(
            desiredPosition.x,
            minLimit.x + camWidth,
            maxLimit.x - camWidth
        );

        float clampedY = Mathf.Clamp(
            desiredPosition.y,
            minLimit.y + camHeight,
            maxLimit.y - camHeight
        );

        Vector3 targetPos = new Vector3(
            clampedX,
            clampedY,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            smoothSpeed * Time.deltaTime
        );
    }
}
