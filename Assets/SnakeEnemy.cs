using UnityEngine;

public class SnakeEnemy : MonoBehaviour
{
    public int damage = 1;
    public bool isDead = false;

    private Animator anim;
    private Collider2D[] colliders;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        colliders = GetComponentsInChildren<Collider2D>();
    }

    public void TakeHit()
    {
        if (isDead) return;

        isDead = true;

        if (anim != null)
            anim.SetTrigger("Die");

        foreach (Collider2D col in colliders)
            col.enabled = false;

        Destroy(gameObject, 0.3f);
    }

    public void DamagePlayer(GameObject playerObj)
    {
        if (isDead) return;

        Player player = playerObj.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}