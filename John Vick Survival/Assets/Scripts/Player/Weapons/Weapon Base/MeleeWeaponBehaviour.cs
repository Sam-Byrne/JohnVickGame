using UnityEngine;

public class MeleeWeaponBehaviour : MonoBehaviour
{
    public float destroyAfterSeconds;
    public WeaponScriptableObject weaponData;
    // current stats of the melee weapon
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;
    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;

        // NEW: fold in player runtime bonuses
        var p = FindObjectOfType<PlayerStats>();
        if (p != null)
        {
            currentDamage += p.currentDamage;
            // speed rarely matters for a melee hitbox, but keep parity:
            currentSpeed += p.currentProjectileSpeed;
        }
    }



    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            PlayerStats p = FindObjectOfType<PlayerStats>();

            float dmg = currentDamage;                 
            if (p != null) dmg *= p.damageMultiplier;  

            if (p != null && Random.value <= p.critChance)
                dmg *= p.critDamage;                   

            enemy.TakeDamage(dmg);
        }
    }


}
