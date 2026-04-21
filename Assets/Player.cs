using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;

    private bool isGrounded;
    private bool onLadder;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (onLadder)
        {
            rb.linearVelocity = new Vector2(move * speed, vertical * speed);
        }
        else
        {
            rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
        }
    }

    // chão
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    // escada
    void SetLayerCollision(string layerName, bool ignore)
    {
        Physics2D.IgnoreLayerCollision(
            gameObject.layer,
            LayerMask.NameToLayer(layerName),
            ignore
        );
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ladder"))
        {
            onLadder = true;
            rb.gravityScale = 0;

            // 👇 permite atravessar ambos enquanto está na escada
            SetLayerCollision("GroundTop", true);
            SetLayerCollision("GroundBottom", true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Ladder"))
        {
            onLadder = false;
            rb.gravityScale = 1;

            // 👇 detecta em qual chão você está
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("GroundBottom"))
                {
                    // ativa só o de baixo
                    SetLayerCollision("GroundBottom", false);
                    SetLayerCollision("GroundTop", true);
                }
                else
                {
                    // ativa só o de cima
                    SetLayerCollision("GroundTop", false);
                    SetLayerCollision("GroundBottom", true);
                }
            }
        }
    }
}