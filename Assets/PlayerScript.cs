using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    private Rigidbody2D body;
    public float speed = 5f;
    public float jumpForce = 7f;
    private bool isGrounded;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(4, 4, 4); ;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-4, 4, 4);
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            isGrounded = false;
        }


        body.transform.rotation = Quaternion.identity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
