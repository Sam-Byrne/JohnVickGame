using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStats player;
    CircleCollider2D playerCollector;
    public float pullSpeed = 5f;

    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        playerCollector = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        playerCollector.radius = player.CurrentMagnet;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.TryGetComponent(out ICollectable collectable) &&
            col.TryGetComponent(out Pickup pickup) && !pickup.isCollected)
        {
            Rigidbody2D rb = col.attachedRigidbody;
            if (rb != null)
            {
                Vector2 forceDirection = (transform.position - col.transform.position).normalized;
                rb.AddForce(forceDirection * pullSpeed);
            }

            if (Vector2.Distance(transform.position, col.transform.position) < 0.3f)
            {
                pickup.isCollected = true; // mark before Collect()
                collectable.Collect();
            }
        }
    }
}