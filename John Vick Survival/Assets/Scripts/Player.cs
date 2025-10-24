using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    float speedX, speedY;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal");
        speedY = Input.GetAxisRaw("Vertical");
        rb.linearVelocity = new Vector2(speedX, speedY).normalized * moveSpeed;
        
    }
}
