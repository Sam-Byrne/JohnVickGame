using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    float speedX, speedY;
    Rigidbody2D rb;
    Vector2 moveDir;
    public Vector2 MoveDir => moveDir;
    public Animator animator;
    int direction = 1;
    PlayerMovement pm;
    public float lastHorizontalVector;
    public float lastVerticalVector;
    public Vector2 lastMovedVector;
    public bool alive;

    public CharacterScriptableObject characterData;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();
        lastMovedVector = new Vector2(1, 0f);
        alive = true;
    }


    void Update()
    {
        if (alive == true)
        {

            speedX = Input.GetAxisRaw("Horizontal");
            speedY = Input.GetAxisRaw("Vertical");
            moveDir = new Vector2(speedX, speedY).normalized; // Update moveDir here
            rb.linearVelocity = moveDir * moveSpeed; // Use rb.velocity instead of rb.linearVelocity

            animator.SetFloat("Horizontal", speedX);
            animator.SetFloat("Vertical", speedY);
            animator.SetFloat("Speed", speedX);
            animator.SetInteger("Direction", direction);
        }
        else if (alive == false)
        {
            animator.SetBool("Alive",  false);
        }

        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f);
        }

        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector);
        }

        if (moveDir.x !=0 && moveDir.y !=0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector);
        }
        
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
