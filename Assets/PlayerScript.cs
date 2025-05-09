using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    public float speed = 5f;
    public float jumpForce = 7f;
    private bool isGrounded;
    private bool onIce = false;
    private float velocityXSmoothing;
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();

        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Level 2")
        {
            onIce = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded)
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
                isGrounded = false;
            }
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(4, 4, 4);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-4, 4, 4);
        }

        body.transform.rotation = Quaternion.identity;
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (onIce)
        {
            float targetVelocityX = horizontalInput * speed;
            float smoothedX = Mathf.SmoothDamp(body.linearVelocity.x, targetVelocityX, ref velocityXSmoothing, 0.3f);
            body.linearVelocity = new Vector2(smoothedX, body.linearVelocity.y);
        }
        else
        {
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Ice"))
        {
            isGrounded = true;
        }
    }
}
