using UnityEngine;
using UnityEngine.EventSystems;

// Orbit camera: hold right mouse button and drag to orbit, scroll wheel to zoom.
public class OrbitCamera : MonoBehaviour
{
    public Vector3 target = Vector3.zero;
    public float distance = 14f;
    public float minDistance = 3f;
    public float maxDistance = 40f;
    public float rotateSpeed = 4f;
    public float zoomSpeed = 2f;

    float yaw = 35f;
    float pitch = 25f;

    void LateUpdate()
    {
        bool overUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        if (!overUI)
        {
            if (Input.GetMouseButton(1))
            {
                yaw += Input.GetAxis("Mouse X") * rotateSpeed;
                pitch -= Input.GetAxis("Mouse Y") * rotateSpeed;
                pitch = Mathf.Clamp(pitch, -89f, 89f);
            }
            distance = Mathf.Clamp(distance - Input.mouseScrollDelta.y * zoomSpeed, minDistance, maxDistance);
        }

        Quaternion rot = Quaternion.Euler(pitch, yaw, 0f);
        transform.position = target + rot * new Vector3(0f, 0f, -distance);
        transform.rotation = rot;
    }
}
