using UnityEngine;

public class UnityChanController : MonoBehaviour
{
    public CameraController cam;
    private Animator animator;
    private float smoothTime = 0.05f;
    private Vector3 velocity = Vector3.zero;
    private bool leftPosition = true;
    private bool movingRight = false;
    private bool movingLeft = false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && !(movingLeft || movingRight) && cam.earthFocus)
        {

            if (leftPosition)
            {
                movingRight = true;
                animator.Play("RUN00_R", -1, 0.0f);
            }
            else
            {
                movingLeft = true;
                animator.Play("RUN00_L", -1, 0.0f);
            }

        }

        if (movingRight || movingLeft)
        {
            Vector3 targetPosition = this.transform.position;
            if (movingLeft)
            {
                targetPosition = new Vector3(-1.5f, 0, 0);
            }
            else
            {
                targetPosition = new Vector3(1.5f, 0, 0);
            }
            // Smoothly move the character towards that target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            if (this.transform.position.x < -1.49f)
            {
                movingLeft = false;
                leftPosition = true;
            }
            else if (this.transform.position.x > 1.49f)
            {
                movingRight = false;
                leftPosition = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rocket")
        {
            animator.Play("Collided", -1, 0.0f);
        }
    }
}
