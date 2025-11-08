using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3.5f;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    Rigidbody2D rb;
    Vector2 moveInput;

    public Vector2 LastAimDir { get; private set; } = Vector2.right;

    public bool alive = true;
    public Vector2 lastFacing = Vector2.right;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (animator == null) animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {   
        if (!alive)
        {
            animator.SetBool("Alive", false);
            rb.linearVelocity = Vector2.zero;
            return;
        }

        
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput = moveInput.normalized;
        rb.linearVelocity = moveInput * moveSpeed;


        animator.SetFloat("Speed", rb.linearVelocity.sqrMagnitude);
        
        if (moveInput.x > 0f)
            lastFacing = Vector2.right;
        else if (moveInput.x < 0f)
            lastFacing = Vector2.left; 

        
        if (moveInput.sqrMagnitude > 0.01f)
        {
            LastAimDir = QuantizeTo8(moveInput);
        }

        // Flip sprite by facing (based on last aim)
        if (LastAimDir.x > 0.01f)  spriteRenderer.flipX = false;
        if (LastAimDir.x < -0.01f) spriteRenderer.flipX = true;
    }

    
    Vector2 QuantizeTo8(Vector2 v)
    {
        if (v.sqrMagnitude < 0.0001f) return LastAimDir;

        float a = Mathf.Atan2(v.y, v.x);                 
        float sector = Mathf.Round(a / (Mathf.PI / 4f)); 
        float snapped = sector * (Mathf.PI / 4f);

        return new Vector2(Mathf.Cos(snapped), Mathf.Sin(snapped)).normalized;
    }


    public void Die()
    {
        alive = false;
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("Alive", false);
    }


}
