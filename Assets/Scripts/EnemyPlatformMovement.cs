using UnityEngine;

public class EnemyPlatformMovement : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 5f;
    public bool moveVertically = false;  

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
}
