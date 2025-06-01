using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float detectionRange = 10f;
    public float speed = 3f;

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;

    private bool hasDetectedPlayer = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rb.gravityScale = 0; 
        rb.isKinematic = true; 
    }

    void FixedUpdate()
    {
        if (player == null)
            return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            if (!hasDetectedPlayer)
            {
                animator.SetTrigger("PlayerDetected");
                hasDetectedPlayer = true;
            }

            rb.isKinematic = false;

            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
        else
        {
            if (hasDetectedPlayer)
            {
                hasDetectedPlayer = false;
            }

            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f; 
            rb.rotation = 0f;        
            rb.isKinematic = true;   
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 contactPoint = collision.contacts[0].point;
            Vector2 center = GetComponent<Collider2D>().bounds.center;

            bool hitFromAbove = contactPoint.y > center.y + 0.2f;

            if (hitFromAbove)
            {
                Destroy(gameObject);

                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 10f); 
            }
        }
    }

}
