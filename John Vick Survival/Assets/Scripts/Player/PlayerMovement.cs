using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    float speedX, speedY;
    
    Rigidbody2D rb;

    Vector2 moveDir;
    public Vector2 MoveDir => moveDir; // Add this line to expose moveDir

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
        moveDir = new Vector2(speedX, speedY).normalized; // Update moveDir here
        rb.linearVelocity = moveDir * moveSpeed; // Use rb.velocity instead of rb.linearVelocity

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
