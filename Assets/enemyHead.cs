// using UnityEngine;

// public class EnemyHead : MonoBehaviour
// {
//     public SnakeEnemy enemy;
//     public float bounceForce = 10f;

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (!other.CompareTag("Player")) return;

//         Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
//         if (rb != null)
//         {
//             enemy.TakeHit();
//             rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceForce);
//         }
//     }
// }
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    public SnakeEnemy enemy;
    public float bounceForce = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            enemy.TakeHit();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceForce);
        }
    }
}