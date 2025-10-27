using UnityEngine;

public class XP : MonoBehaviour, ICollectable
{
    public int experienceGranted;
    public void Collect()
    {

        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseExperience(experienceGranted);
        Destroy(gameObject);
    }


}
