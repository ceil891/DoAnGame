using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    public Transform startPoint;
    public float respawnDelay = 0.8f;

    Rigidbody2D rb;
    Animator anim;
    PlayerMovement movement;
    Collider2D col;

    bool isDead;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        col = GetComponent<Collider2D>();
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        // ❌ Tắt toàn bộ điều khiển
        movement.enabled = false;

        // ❌ Tắt collider để không dính ground
        col.enabled = false;

        // 🎬 Play hit animation
        anim.SetBool("IsHit", true);

        // Reset vật lý
        rb.velocity = Vector2.zero;
        rb.gravityScale = 3f;

        // Hất người chơi lên rồi rơi
        rb.AddForce(new Vector2(0, 9f), ForceMode2D.Impulse);

        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnDelay);

        // Tele về start
        transform.position = startPoint.position;

        // Reset vật lý
        rb.velocity = Vector2.zero;
        rb.gravityScale = 1.5f;

        // Bật lại
        col.enabled = true;
        movement.enabled = true;

        anim.SetBool("IsHit", false);
        isDead = false;
    }
}
