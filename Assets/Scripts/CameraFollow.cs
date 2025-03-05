using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Assign the player GameObject
    public float smoothSpeed = 5f; // How fast the camera moves

    void LateUpdate()
    {
        if (target == null) return;

        // Follow target (only X & Y for 2D games)
        Vector3 newPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed * Time.deltaTime);
    }
}
