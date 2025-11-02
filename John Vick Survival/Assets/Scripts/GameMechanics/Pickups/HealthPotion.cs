using UnityEngine;

public class HealthPotion : MonoBehaviour, ICollectable
{
    public int healAmount = 10; // Set different values per prefab in Inspector
    public float pullSpeed = 8f;

    Transform player;
    bool isBeingPulled = false;

    public void Collect()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        isBeingPulled = true;
    }

    void Update()
    {
        if (!isBeingPulled) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            pullSpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, player.position) < 0.2f)
        {
            PlayerStats p = player.GetComponent<PlayerStats>();
            p.currentHealth = Mathf.Min(p.currentHealth + healAmount, p.maxHealth);
            Destroy(gameObject);
        }
    }
}
