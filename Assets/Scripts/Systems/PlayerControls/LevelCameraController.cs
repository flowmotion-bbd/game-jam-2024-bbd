using UnityEngine;

public class LevelCameraController : MonoBehaviour
{
    public float panSpeed = 1.2f;           // Speed of camera movement
    public float zoomSpeed = 10f;          // Speed of zooming
    public float minZoom = 3f;             // Minimum zoom level
    public float maxZoom = 10f;            // Maximum zoom level

    private Vector3 origin;
    private Vector3 difference;
    private Vector3 resetCamera;

    private bool drag = false;

    private void Start()
    {
        resetCamera = Camera.main.transform.position;
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;

            if (!drag)
            {
                drag = true;
                origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        } else
        {
            drag = false;
        }

        if (drag)
        {
            Camera.main.transform.position = origin - difference * panSpeed;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize -= scroll * zoomSpeed;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
    }
}
