using UnityEngine;

public class EnemyFollowByDistance : MonoBehaviour
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

            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
        else
        {
            hasDetectedPlayer = false; 
        }
    }
}
