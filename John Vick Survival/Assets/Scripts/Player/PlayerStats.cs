using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static Action OnLevelUpUI;
    public AudioSource musicSource;
    public DeathScreenController deathScreenController;




    public CharacterScriptableObject characterData;

    [Header("Runtime Stats")]
    public float attackSpeedMultiplier = 1f;
    public float maxHealth;
    public float currentHealth;
    public float currentProjectileSpeed;
    public float currentRecovery;
    public float currentMagnet;
    public float damageMultiplier = 1f;

    public float bonusPierce = 0;


    public List<GameObject> spawnedWeapons = new List<GameObject>();

    public int level = 1;
    public int experience = 0;
    public int experienceCap = 10;
    public int experienceCapIncrease = 5;

    [Header("Damage / IFrames")]
    public float invulnDuration = 0.2f;
    //private float invulnTimer = 0f;
    public bool invulnerable = false;

    [Header("Critical Stats")]
    public float critChance = 0.01f;
    public float critDamage = 1.25f;


    public List<PassiveItemScriptableObject> passiveItems = new List<PassiveItemScriptableObject>();
    public int maxPassiveSlots = 3;


    [Header("Audio")]
    public AudioSource sfxSource; 
    public AudioClip damageSFX;  
    public AudioClip heartbeatNormal;   
    public AudioClip heartbeatIntense;        
    public AudioClip deathSFX;             
    public AudioClip levelUpSound;
    [Range(0f, 1f)] public float levelUpVolume = 1f;




    public bool alive = true;
    PlayerMovement pm;

    public GameObject PlayerDeathEffect;



    public int kills = 0;

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }


    void Awake()
    {
        Time.timeScale = 1f;

        // coming from character select
        if (CharacterSelector.instance != null)
        {
            characterData = CharacterSelector.GetData();
            CharacterSelector.instance.DestroySingleton();
        }
        else
        {
            Debug.LogWarning("CharacterSelector missing -> Using inspector-assigned characterData.");
        }

        // runtime stat stuff

        maxHealth = characterData.MaxHealth;
        currentHealth = maxHealth;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        currentRecovery = characterData.Recovery;
        currentMagnet = characterData.Magnet;

        // apply move speed to movement script
        GetComponent<PlayerMovement>().moveSpeed = characterData.MoveSpeed;


        if (characterData.StartingWeapon != null)
        {
            GameObject w = Instantiate(characterData.StartingWeapon, transform).gameObject;
            spawnedWeapons.Add(w);
        }

    }

    void Update()
    {
        RecoverHP();
        CheckLevelUp();
    }

    void RecoverHP()
    {
        if (currentRecovery > 0 && currentHealth < maxHealth)
        {
            currentHealth += currentRecovery * Time.deltaTime;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
        }
    }

    void CheckLevelUp()
    {
        if (experience >= experienceCap)
        {
            if (levelUpSound != null)
            {
                var sfx = new GameObject("LevelUpSFX").AddComponent<AudioSource>();
                sfx.spatialBlend = 0f;
                sfx.volume = levelUpVolume;
                sfx.PlayOneShot(levelUpSound);
                Destroy(sfx.gameObject, levelUpSound.length);
            }
    
            level++;
            experience -= experienceCap;
            experienceCap += experienceCapIncrease;

            Time.timeScale = 0f;
            OnLevelUpUI?.Invoke();
        }
    }

    public void TakeDamage(float dmg)
    {
        if (invulnerable) return;

        currentHealth -= dmg;
        if (sfxSource && damageSFX)
            sfxSource.PlayOneShot(damageSFX);
        float hpPercent = currentHealth / maxHealth;
        if (sfxSource)
        {
            if (hpPercent <= 0.25f && heartbeatIntense)
                sfxSource.PlayOneShot(heartbeatIntense);
            else if (hpPercent > 0.25f && heartbeatNormal && hpPercent < 0.50f)
                sfxSource.PlayOneShot(heartbeatNormal);
        }

        HUDController hud = FindObjectOfType<HUDController>();
        if (hud != null) hud.FlashDamage();



        if (currentHealth <= 0.99f)
        {
            currentHealth = 0;
            Die();
            return;
        }

        StartCoroutine(InvulnerabilityFrames());
    }

    void Die()
    {
        alive = false;
        if (pm != null) pm.enabled = false;

        if (sfxSource && deathSFX)
            sfxSource.PlayOneShot(deathSFX);

        Instantiate(PlayerDeathEffect, transform.position, Quaternion.identity);
        HUDController hud = FindObjectOfType<HUDController>();
        if (hud != null) hud.StopTimer();

        FindObjectOfType<MusicFadeController>().SlowMusic();

        if (deathScreenController != null)
            deathScreenController.TriggerGameOver();
        else
            Debug.LogError("DeathScreenController is NOT assigned to PlayerStats!");

        Destroy(gameObject);
    }


    public void IncreaseExperience(int amount) => experience += amount;
    public void AddKill() => kills++;

    System.Collections.IEnumerator InvulnerabilityFrames()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulnDuration);
        invulnerable = false;
    }

    public void GainMaxHealth(float amount)
    {
        maxHealth += amount;
        currentHealth += amount * 0.8f; // small heal bonus when max health rises
    }

    public void GainDamagePercent(float percentIncrease)
    {
    
        damageMultiplier += percentIncrease;
    }



    public void GainMagnet(float amount)
    {
        currentMagnet += amount;
    }

    public void GainProjectileSpeed(float amount)
    {
        currentProjectileSpeed += amount;
    }

    public void GainMoveSpeed(float amount)
    {
        PlayerMovement pm = GetComponent<PlayerMovement>();
        pm.moveSpeed += amount;
    }

    public void GainAttackSpeed(float percentIncrease)
    {
        attackSpeedMultiplier += percentIncrease;
    }

    public void GainCritChance(float amount)
    {
        critChance += amount;
        critChance = Mathf.Clamp(critChance, 0f, 1f);
    }

    public void GainCritDamage(float amount)
    {
        critDamage += amount;
    }

    public void GainRecovery(float amount)
    {
        currentRecovery += amount;
    }

    public void GainPierce(float amount)
    {
        bonusPierce += amount;
    }



    //passive item stuff
    public void ApplyPassiveEffect(PassiveItemScriptableObject item, int level)
    {
        float value = item.upgradeValues[Mathf.Clamp(level, 0, item.upgradeValues.Length - 1)];

        switch (item.passiveType)
        {
            case PassiveItemScriptableObject.PassiveType.MaxHealth:
                GainMaxHealth(value);
                break;

            case PassiveItemScriptableObject.PassiveType.DamageMultiplier:
                GainDamagePercent(value);
                break;

            case PassiveItemScriptableObject.PassiveType.MoveSpeed:
                GainMoveSpeed(value);
                break;

            case PassiveItemScriptableObject.PassiveType.AttackSpeed:
                GainAttackSpeed(value);
                break;

            case PassiveItemScriptableObject.PassiveType.CritChance:
                GainCritChance(value);
                break;

            case PassiveItemScriptableObject.PassiveType.CritDamage:
                GainCritDamage(value);
                break;

            case PassiveItemScriptableObject.PassiveType.Magnet:
                GainMagnet(value);
                break;

            case PassiveItemScriptableObject.PassiveType.Recovery:
                GainRecovery(value);
                break;

            case PassiveItemScriptableObject.PassiveType.bonusPierce:
                GainPierce(value);
                break;    
        }
    }

    private Dictionary<PassiveItemScriptableObject, int> passiveLevels = new Dictionary<PassiveItemScriptableObject, int>();
    public void GainPassiveItem(PassiveItemScriptableObject item)
    {
        if (!passiveLevels.ContainsKey(item))
        {
            passiveLevels[item] = 1;
            passiveItems.Add(item);
        }
        else
        {
            passiveLevels[item]++;
        }

        ApplyPassiveEffect(item, passiveLevels[item] - 1);

        FindObjectOfType<HUDController>().RefreshPassiveHUD(this);
    }

    public int GetPassiveLevel(PassiveItemScriptableObject item)
    {
        if (passiveLevels.ContainsKey(item))
            return passiveLevels[item];

        return 0;
    }


}
