using UnityEngine;

public class EnemyPlatformMovement : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 5f;

    private float startX;
    private int direction = 1;

    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * direction * Time.deltaTime);

        if (Mathf.Abs(transform.position.x - startX) >= moveDistance)
        {
            direction *= -1;
        }
    }
}

