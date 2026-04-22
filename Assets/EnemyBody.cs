// using UnityEngine;

// public class EnemyBody : MonoBehaviour
// {
//     public SnakeEnemy enemy;

//     private void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (!collision.gameObject.CompareTag("Player")) return;

//         enemy.DamagePlayer(collision.gameObject);
//     }
// }

using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    public SnakeEnemy enemy;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        enemy.DamagePlayer(collision.gameObject);
    }
}