using UnityEngine;

public class IsometricCameraFollow : MonoBehaviour
{
    public Transform player; // Player reference
    public Vector3 offset = new Vector3(-10f, 10f, -10f); // Adjust for isometric view
    public float moveSpeed = 5f; // Speed at which camera moves
    public float boundaryThreshold = 0.3f; // When to move the camera (screen percentage)

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 screenPoint = cam.WorldToViewportPoint(player.position);

        // If player is too far out of screen bounds, adjust camera position diagonally
        if (screenPoint.x < boundaryThreshold || screenPoint.x > 1 - boundaryThreshold ||
            screenPoint.y < boundaryThreshold || screenPoint.y > 1 - boundaryThreshold)
        {
            Vector3 targetPosition = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
