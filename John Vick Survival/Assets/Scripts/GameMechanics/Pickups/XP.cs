using UnityEngine;

public class XP : Pickup, ICollectable
{
    public int experienceGranted;
    public void Collect()
    {

        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseExperience(experienceGranted);

    }

    


}
