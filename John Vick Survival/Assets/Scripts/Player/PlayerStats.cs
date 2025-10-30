using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{

    CharacterScriptableObject characterData;

    // current stats

    float currentHealth;
    float currentRecovery;
    float currentDamage;
    float currentProjectileSpeed;
    float currentMagnet;

    public float CurrentMagnet => currentMagnet;



    // held weapons 
    public List<GameObject> spawnedWeapons;



    // xp and level
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap = 5;
    public int experienceCapIncrease;


    // i frames
    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    void Awake()
    {
        characterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();

        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentDamage = characterData.Damage;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        currentMagnet = characterData.Magnet;

        // starting weapon
        SpawnWeapon(characterData.StartingWeapon);
    }

    void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else
        {
            isInvincible = false;
        }

        Recover();
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

    public void TakeDamage(float dmg)
    {

        if (!isInvincible)
        {
            currentHealth -= dmg;

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
            if (currentHealth <= 0)
            {
                Kill();
            }
        }
    }

    public void Kill()
    {
        Debug.Log("Player Died");
        FindObjectOfType<PlayerMovement>().alive = false;

    }




    public void RestoreHealth(float amount)
    {
        if (currentHealth < characterData.MaxHealth)
        {
            currentHealth += amount;

            if (currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
    }

    void Recover()
    {
        if (currentHealth < characterData.MaxHealth)
        {
            currentHealth += currentRecovery * Time.deltaTime;

            if (currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        spawnedWeapons.Add(spawnedWeapon);

    }

    [Header("Gameplay Stats")]
    public int kills = 0;

    public void AddKill()
    {
        kills++;
    }


    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return characterData.MaxHealth;
    }

}
