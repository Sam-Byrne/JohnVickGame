using UnityEngine;
using System;
using System.Collections;
public class PlayerStats : MonoBehaviour
{
    public static Action OnLevelUpUI;

    public CharacterScriptableObject characterData;

    [Header("Runtime Stats")]
    public float maxHealth;
    public float currentHealth;
    public float currentDamage;
    public float currentProjectileSpeed;
    public float currentRecovery;
    public float currentMagnet;

    public int level = 1;
    public int experience = 0;
    public int experienceCap = 10;
    public int experienceCapIncrease = 5;

    [Header("Damage / IFrames")]
    public float invulnDuration = 0.2f;
    private float invulnTimer = 0f;
    public bool invulnerable = false;


    public bool alive = true;        
    PlayerMovement pm;               



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
        currentDamage = characterData.Damage;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        currentRecovery = characterData.Recovery;
        currentMagnet = characterData.Magnet;

        // apply move speed to movement script
        GetComponent<PlayerMovement>().moveSpeed = characterData.MoveSpeed;


        if (characterData.StartingWeapon != null)
            Instantiate(characterData.StartingWeapon, transform);
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
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
            return;
        }

        StartCoroutine(InvulnerabilityFrames());
    }


    void Die()
    {
        if (!alive) return;
        alive = false;

        pm.Die(); 

        Animator anim = GetComponentInChildren<Animator>();
        anim.updateMode = AnimatorUpdateMode.UnscaledTime; 
        anim.Play("Player_Death", 0, 0f); 

        StartCoroutine(DeathSequence());
    }


    IEnumerator DeathSequence()
    {
        Animator anim = GetComponentInChildren<Animator>();

        anim.updateMode = AnimatorUpdateMode.UnscaledTime;

        anim.SetBool("Alive", false);
        anim.Play("Player_Death", 0, 0f); 

        yield return new WaitForSecondsRealtime(anim.GetCurrentAnimatorStateInfo(0).length);

        Time.timeScale = 0f;

    }


    public void IncreaseExperience(int amount) => experience += amount;
    public void AddKill() => kills++;

    System.Collections.IEnumerator InvulnerabilityFrames()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulnDuration);
        invulnerable = false;
    }


}
