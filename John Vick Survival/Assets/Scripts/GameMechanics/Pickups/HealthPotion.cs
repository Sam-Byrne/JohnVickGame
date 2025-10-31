using UnityEngine;

public class HealthPotion : Pickup, ICollectable
{
    public int healthToRestore;

    public void Collect()
    {
        if (isCollected) return;
        isCollected = true;
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.RestoreHealth(healthToRestore);
        Destroy(gameObject);
    }
}
