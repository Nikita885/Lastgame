using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterMovement : MonoBehaviour
{
    public float speed = 2f;
    public Transform wallCheck;
    public Transform groundCheck;
    public float checkRadius = 0.1f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool movingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocity = new Vector2((movingRight ? 1 : -1) * speed, rb.linearVelocity.y);

        bool hittingWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, groundLayer);
        bool groundAhead = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        if (hittingWall || !groundAhead)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Перезагрузка сцены
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (wallCheck != null)
            Gizmos.DrawWireSphere(wallCheck.position, checkRadius);
        if (groundCheck != null)
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
