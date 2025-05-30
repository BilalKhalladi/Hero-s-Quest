using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
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
    public GameObject[] hearts;
    public AudioClip jumpSound;
    public AudioClip barrelExplode;
    public AudioClip coinSound;
    public AudioClip heartSound;
    public AudioClip keySound;
    public AudioClip damageSound;

    private AudioSource audioSource;


    private Vector3 spawnPoint;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (SceneManager.GetActiveScene().name == "Level 2")
            onIce = true;

        spawnPoint = transform.position;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            isGrounded = false;
            animator.SetBool("Grounded", false);
            PlaySoundByTag("Jump");

        }

        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput > 0.01f) transform.localScale = new Vector3(4, 4, 4);
        else if (horizontalInput < -0.01f) transform.localScale = new Vector3(-4, 4, 4);

        body.transform.rotation = Quaternion.identity;
        animator.SetBool("Run", horizontalInput != 0);
    }

    void FixedUpdate()
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            if (lives <= 0)
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

        if (collision.gameObject.CompareTag("Key"))
        {
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            PlaySoundByTag(collision.gameObject.tag);
        }

        if (collision.gameObject.CompareTag("Live"))
        {
            if (lives >= hearts.Length - 1)
            {
                Destroy(collision.gameObject);
                PlaySoundByTag(collision.gameObject.tag);
            }
            else
            {
                lives += 1;
                hearts[lives].SetActive(true);
                Destroy(collision.gameObject);
                PlaySoundByTag(collision.gameObject.tag);
            }
        }

        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Ice"))
        {
            isGrounded = true;
            animator.SetBool("Grounded", true);
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            HUDController hud = FindObjectOfType<HUDController>();
            if (hud != null)
            {
                hud.AddCoin(1);
            }
            Destroy(collision.gameObject);
            PlaySoundByTag(collision.gameObject.tag);
        }

        if (collision.gameObject.CompareTag("Button"))
        {
            Animator buttonAnimator = collision.gameObject.GetComponent<Animator>();
            if (buttonAnimator != null)
            {
                buttonAnimator.SetBool("isPressed", true);
            }

            GameObject barrel = GameObject.FindGameObjectWithTag("Barrel");
            if (barrel != null)
            {
                Animator barrelAnimator = barrel.GetComponent<Animator>();
                if (barrelAnimator != null)
                {
                    barrelAnimator.SetTrigger("ExplodeBarrel");
                    GameObject wall = GameObject.FindGameObjectWithTag("Wall");
                    StartCoroutine(DestroyBarrelAndWall(barrel, wall, 0.3f));
                }
            }
        }

        if (collision.gameObject.CompareTag("Door"))
        {
            GameObject key = GameObject.Find("key");

            if (key != null)
            {
                Animator doorAnimator = collision.gameObject.GetComponent<Animator>();
                if (doorAnimator != null)
                {
                    doorAnimator.SetBool("doorOpen", true);
                    StartCoroutine(LoadNextLevelAfterDelay(0.2f));
                }
            }
        }
    }

    private IEnumerator LoadNextLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Buscamos el HUDController en la escena
        HUDController hud = FindObjectOfType<HUDController>();
        if (hud != null)
        {
            hud.GuardarMarca(); // Guardamos los datos antes de cambiar de escena
        }

        SceneManager.LoadScene("Level 2");
    }


    private IEnumerator DestroyBarrelAndWall(GameObject barrel, GameObject wall, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (wall != null)
        {
            wall.SetActive(false);
            PlaySoundByTag(barrel.tag);

        }

        Destroy(barrel);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            spawnPoint = other.transform.position;
            Debug.Log("Checkpoint activado en: " + spawnPoint);
        }
    }

    private IEnumerator DecreaseLivesWithDelay()
    {
        PlaySoundByTag("Trap");
        yield return new WaitForSeconds(0.5f);
        lives -= 1;
    }

    public void TakeDamage()
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
        transform.position = spawnPoint;
        body.linearVelocity = Vector2.zero;

        lives = hearts.Length - 1;
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(true);
        }
    }

    private void PlaySoundByTag(string tag)
    {
        switch (tag)
        {
            case "Coin":
                PlaySound(coinSound);
                break;
            case "Live":
                PlaySound(heartSound);
                break;
            case "Barrel":
                PlaySound(barrelExplode);
                break;
            case "Jump":
                PlaySound(jumpSound);
                break;
            case "Key":
                PlaySound(keySound);
                break;
            case "Trap":
                PlaySound(damageSound);
                break;
            default:
                break;
        }
    }
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
            audioSource.PlayOneShot(clip);
    }
}
