using UnityEngine;

public class SpikeHead : MonoBehaviour
{
    public float attackSpeed = 8f;
    public float attackDistance = 2.5f;

    private Animator animator;
    private Vector2 startPos;
    private Vector2 attackDir;
    private bool attacking = false;
    private bool returning = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        startPos = transform.position;
    }

    void Update()
    {
        if (attacking)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                startPos + attackDir * attackDistance,
                attackSpeed * Time.deltaTime
            );

            if (Vector2.Distance(transform.position, startPos + attackDir * attackDistance) < 0.05f)
            {
                attacking = false;
                returning = true;
            }
        }
        else if (returning)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                startPos,
                attackSpeed * Time.deltaTime
            );

            if (Vector2.Distance(transform.position, startPos) < 0.05f)
            {
                returning = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (attacking || returning) return;
        if (!collision.CompareTag("Player")) return;

        Vector2 dir = (collision.transform.position - transform.position).normalized;

        // Xác định hướng chính
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            attackDir = new Vector2(Mathf.Sign(dir.x), 0);
        else
            attackDir = new Vector2(0, Mathf.Sign(dir.y));

        // Animation theo hướng
        if (attackDir == Vector2.up) animator.SetTrigger("HitTop");
        else if (attackDir == Vector2.down) animator.SetTrigger("HitBottom");
        else if (attackDir == Vector2.left) animator.SetTrigger("HitLeft");
        else if (attackDir == Vector2.right) animator.SetTrigger("HitRight");

        attacking = true;
    }
}
