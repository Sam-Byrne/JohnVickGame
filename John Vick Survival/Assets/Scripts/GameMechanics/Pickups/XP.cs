using UnityEngine;

public class XP : MonoBehaviour, ICollectable
{
    public int xpValue = 1; // base value
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
            player.GetComponent<PlayerStats>().IncreaseExperience(xpValue);
            Destroy(gameObject);
        }
    }
}
