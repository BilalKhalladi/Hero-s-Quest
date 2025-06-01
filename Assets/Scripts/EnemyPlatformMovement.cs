using UnityEngine;

public class EnemyPlatformMovement : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 5f;
    public bool moveVertically = false;  
    public bool IsEnemy = false;  

    private float startPos;
    private int direction = 1;

    void Start()
    {
        startPos = moveVertically ? transform.position.y : transform.position.x;
    }

    void Update()
    {

        if (moveVertically)
        {
            transform.Translate(Vector2.up * speed * direction * Time.deltaTime);

            if (Mathf.Abs(transform.position.y - startPos) >= moveDistance)
            {
                direction *= -1; 
            }
        }
        else
        {
            transform.Translate(Vector2.right * speed * direction * Time.deltaTime);

            if (Mathf.Abs(transform.position.x - startPos) >= moveDistance)
            {
                direction *= -1; 
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && IsEnemy)
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
