using System.Security.Cryptography;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private Animator animator;
    private bool isExploding = false;
    public float damageRadius = 2f;
    private GameObject platform;
    public AudioClip ballonPop;
    private AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        platform = GameObject.Find("PlatformBallon");

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isExploding && collision.gameObject.CompareTag("Player"))
        {
            isExploding = true;
            animator.SetTrigger("Explode");

            GetComponent<Collider2D>().enabled = false;
        }
    }

    private void Update()
    {
        if (isExploding)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("BallonExplode") && stateInfo.normalizedTime >= 1f)
            {
                TryDamagePlayer();
                PlayPopSound();
                Destroy(gameObject);
                Destroy(platform);
            }
        }
    }

    private void TryDamagePlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector2.Distance(player.transform.position, transform.position);
            if (distance <= damageRadius)
            {
                player.GetComponent<PlayerMovement>().TakeDamage();
            }
        }
    }
    void PlayPopSound()
    {
        if (ballonPop != null)
            audioSource.PlayOneShot(ballonPop);
    }
}
