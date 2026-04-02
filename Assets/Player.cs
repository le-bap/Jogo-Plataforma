using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;

    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode jumpKey = KeyCode.Space;

    private Rigidbody2D rb;
    private Animator animator;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float move = 0;

        if (Input.GetKey(leftKey))
            move = -1;

        if (Input.GetKey(rightKey))
            move = 1;

        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

       if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(DropDown());
        }

        if (move > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (move < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    IEnumerator DropDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);

        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            Collider2D playerCol = GetComponent<Collider2D>();
            Collider2D platformCol = hit.collider;

            Physics2D.IgnoreCollision(playerCol, platformCol, true);

            yield return new WaitForSeconds(0.3f);

            Physics2D.IgnoreCollision(playerCol, platformCol, false);
        }
    }
}