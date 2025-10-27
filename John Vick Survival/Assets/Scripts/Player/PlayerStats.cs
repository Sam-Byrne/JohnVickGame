using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public CharacterScriptableObject characterData;

    // current stats

    float currentHealth;
    float currentRecovery;
    float currentMight;
    float currentProjectileSpeed;


    // xp and level
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap = 5;
    public int experienceCapIncrease;


    void Awake()
    {
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUpChecker();
    }
    
    void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;
            experienceCap += experienceCapIncrease;
        }
    }

}
