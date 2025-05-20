using System.Collections;
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
    private Animator animator;
    private int lives = 3;
    public GameObject [] hearts;
    

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

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
                animator.SetBool("Grounded", false);
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

        animator.SetBool("Run", horizontalInput != 0);
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
        if (collision.gameObject.CompareTag("Trap"))
        {
        
            if(lives <= 0)
            {
                RestartLevel();
            }
            else
            {
                hearts[lives].SetActive(false);
                StartCoroutine(DecreaseLivesWithDelay());
            }

        }

        if (collision.gameObject.CompareTag("Limit"))
        {
            RestartLevel();
        }

        if (collision.gameObject.CompareTag("Live"))
        {
            if (lives >= 3)
            {
                Destroy(collision.gameObject);

            }
            else
            {
                lives += 1;
                hearts[lives].SetActive(true);
                Destroy(collision.gameObject);
            }
            
        }

        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Ice"))
        {
            isGrounded = true;
            animator.SetBool("Grounded", true);
        }
    }

    private IEnumerator DecreaseLivesWithDelay()
    {
        yield return new WaitForSeconds(1f); 
        lives -= 1; 
    }
    public void TakeDamage() //For ballon explosion
    {
        if (lives <= 0)
        {
            RestartLevel();
        }
        else
        {
            hearts[lives].SetActive(false);
            lives -= 1;
        }
    }


    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }



}
