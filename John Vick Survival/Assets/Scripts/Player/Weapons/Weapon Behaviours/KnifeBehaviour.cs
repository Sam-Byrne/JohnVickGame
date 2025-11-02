using UnityEngine;

public class KnifeBehaviour : MonoBehaviour
{
    float damage;
    Vector2 direction;
    float moveSpeed;

    public void Initialize(float dmg, Vector2 dir, float speed)
    {
        damage = dmg;
        direction = dir.normalized;
        moveSpeed = speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 45f);
    }
    
    void Start()
    {
        Destroy(gameObject, 6f); // destroy after 6 seconds if nothing hits
    }



    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out EnemyStats enemy))
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        if (col.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
    }
}
