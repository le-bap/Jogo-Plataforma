using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Player : MonoBehaviour
{
    [Header("Movimento")]
    public float speed = 5f;
    public float jumpForce = 10f;

    [Header("Vidas")]
    public int lives = 3;
    public TMP_Text livesText;
    public float invulnerableTime = 1f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool onLadder;
    private bool isInvulnerable = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UpdateLivesUI();
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

    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return;

        lives -= damage;

        if (lives < 0)
            lives = 0;

        Debug.Log("Tomou dano! Vidas restantes: " + lives);
        UpdateLivesUI();

        if (lives <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(InvulnerabilityCoroutine());
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Vidas: " + lives;
        }
    }

    void Die()
    {
        SceneManager.LoadScene("tela derrota");
    }

    IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerableTime);
        isInvulnerable = false;
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

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("GroundBottom"))
                {
                    SetLayerCollision("GroundBottom", false);
                    SetLayerCollision("GroundTop", true);
                }
                else
                {
                    SetLayerCollision("GroundTop", false);
                    SetLayerCollision("GroundBottom", true);
                }
            }
        }
    }
}