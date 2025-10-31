using UnityEngine;

public class XP : Pickup, ICollectable
{
    public int experienceGranted;
    
    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseExperience(experienceGranted);

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        Destroy(gameObject);
    }


}
