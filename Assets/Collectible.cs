using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int points = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.AddPoints(points);
            }

            Destroy(gameObject);
        }
    }
}