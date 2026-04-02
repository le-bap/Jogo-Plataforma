using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float lenght;
    public float parallaxEffect;

    public KeyCode rightKey = KeyCode.D;
    public KeyCode leftKey = KeyCode.A;

    void Start()
    {
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float direction = 0;

        if (Input.GetKey(rightKey))
            direction = -1; // fundo vai pro lado contrário

        if (Input.GetKey(leftKey))
            direction = 1;

        transform.position += Vector3.right * direction * Time.deltaTime * parallaxEffect;

        // Loop do background
        if (transform.position.x < -lenght)
        {
            transform.position = new Vector3(lenght, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > lenght)
        {
            transform.position = new Vector3(-lenght, transform.position.y, transform.position.z);
        }
    }
}