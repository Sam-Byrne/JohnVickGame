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
        var p = FindObjectOfType<PlayerStats>();
        currentDamage = weaponData.Damage + p.currentDamage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;

        
        if (p != null)
        {
            currentDamage += p.currentDamage;
            
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

            bool crit = false;
            if (p != null && Random.value <= p.critChance)
            {
                dmg *= p.critDamage;
                crit = true;
            }
            enemy.TakeDamage(dmg, crit);


        }
    }


}
