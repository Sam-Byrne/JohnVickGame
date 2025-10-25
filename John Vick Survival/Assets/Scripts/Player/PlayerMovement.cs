using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    float speedX, speedY;
    
    Rigidbody2D rb;

    Vector2 moveDir;

    public Animator animator;
    int direction = 1;
    PlayerMovement pm;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();
    }


    void Update()
    {
        
        speedX = Input.GetAxisRaw("Horizontal");
        speedY = Input.GetAxisRaw("Vertical");
        rb.linearVelocity = new Vector2(speedX, speedY).normalized * moveSpeed;

        animator.SetFloat("Horizontal", speedX);
        animator.SetFloat("Vertical", speedY);
        animator.SetFloat("Speed", speedX);
        animator.SetInteger("Direction", direction);
        
        if (speedX > 0)
        {
            direction = 1;
        }
        else if (speedX < 0)
        {
            direction = -1;
        }
        
    }
}
