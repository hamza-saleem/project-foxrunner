using UnityEngine;

public class SideScrollingCamera : MonoBehaviour
{
    public Transform target;           // The target the camera will follow (e.g., the player)
    public float smoothSpeed = 0.125f; // The smoothing speed of the camera movement

    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        if (target != null)
        {
            offset = transform.position - target.position;
        }
        else
        {
            Debug.LogError("No target assigned to the SideScrollingCamera!");
        }
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Slerp(transform.position, targetPosition, smoothSpeed);
            //Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
            transform.position = new Vector3(smoothedPosition.x, 0, transform.position.z);
        }
    }
}
