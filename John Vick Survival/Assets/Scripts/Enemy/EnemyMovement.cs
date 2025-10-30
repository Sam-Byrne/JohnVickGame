using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // no longer needed (keeping just in case) public EnemyScriptableObject enemyData;
    EnemyStats enemy;
    Transform player;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindObjectOfType<PlayerMovement>().transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime);

        if (spriteRenderer != null)
        {
            if (player.position.x < transform.position.x)
                spriteRenderer.flipX = true;  
            else
                spriteRenderer.flipX = false; 
        }
    }
}
