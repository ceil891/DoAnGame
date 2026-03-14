using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float bounceForce = 12f;
    private Animator animator;
    public AudioSource trampolineSound;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            }

            animator.SetTrigger("Jump");

            // Âm thanh to hơn
            trampolineSound.PlayOneShot(trampolineSound.clip, 2f);
        }
    }
}
