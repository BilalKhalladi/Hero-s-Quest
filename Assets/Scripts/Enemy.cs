using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireRate = 3f;
    public float fireballSpeed = 5f;
    public bool rightLeft; // true = right, false = left
    public bool down;
    private float nextFireTime = 0f;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            ShootFireball();
            nextFireTime = Time.time + fireRate;
        }
    }

    void ShootFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        Fireball fireballScript = fireball.GetComponent<Fireball>();

        if (fireballScript != null)
        {
            fireballScript.setRightLeft(rightLeft); 
        }

        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();

        if (rightLeft)
        {
            rb.linearVelocity = Vector2.right * fireballSpeed; 
        }
        else
        {
            rb.linearVelocity = Vector2.left * fireballSpeed;
        }

        if (down)
        {
            rb.linearVelocity = Vector2.down * fireballSpeed;
        }
    }
}
