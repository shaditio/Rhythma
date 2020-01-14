using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smoothTime = 0.1F;
    public Color earthColor;
    public Color skyColor;
    public bool earthFocus = true;
    public bool skyFocus = false;
    private Camera cam;
    private Vector3 velocity = Vector3.zero;
    private bool shiftSky = false;
    private bool shiftEarth = false;
    private float rVelocity = 0.0f;
    private float gVelocity = 0.0f;
    private float bVelocity = 0.0f;
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Sky and Earth movement
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (this.transform.position.y <= 6.1)
            {
                shiftSky = true;
                skyFocus = true;
                earthFocus = false;
            }
            else
            {
                shiftEarth = true;
                earthFocus = true;
                skyFocus = false;
            }
        }

        if (shiftEarth || shiftSky)
        {
            Vector3 targetPosition;
            Color targetColor;
            Color holderColor = cam.backgroundColor;
            if (shiftEarth)
            {
                targetPosition = new Vector3(0, 6, -4.2f);
                targetColor = earthColor;
            }
            else
            {
                targetPosition = new Vector3(0, 16, -4.2f);
                targetColor = skyColor;
            }
            // Smoothly move the camera towards that target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            holderColor.r = Mathf.SmoothDamp(cam.backgroundColor.r, targetColor.r, ref rVelocity, smoothTime);
            holderColor.g = Mathf.SmoothDamp(cam.backgroundColor.g, targetColor.g, ref gVelocity, smoothTime);
            holderColor.b = Mathf.SmoothDamp(cam.backgroundColor.b, targetColor.b, ref bVelocity, smoothTime);
            cam.backgroundColor = holderColor;

            if (this.transform.position.y < 6.1)
            {
                shiftEarth = false;
            }
            else if (this.transform.position.y > 15.9)
            {
                shiftSky = false;
            }
        }
    }
}
