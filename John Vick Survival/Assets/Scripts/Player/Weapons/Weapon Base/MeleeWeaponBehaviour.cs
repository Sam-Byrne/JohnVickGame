using UnityEngine;

public class MeleeWeaponBehaviour : MonoBehaviour
{
    public float destroyAfterSeconds;
    public WeaponScriptableObject weaponData;
    // current stats of the melee weapon
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected float currentPierce;
    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce + FindObjectOfType<PlayerStats>().bonusPierce;

    }



    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            PlayerStats p = FindObjectOfType<PlayerStats>();

            float dmg = currentDamage * p.damageMultiplier;

            bool crit = false;
            if (Random.value <= p.critChance)
            {
                dmg *= p.critDamage;
                crit = true;
            }

            enemy.TakeDamage(dmg, crit, weaponData);

            if (weaponData.hitSound != null)
            {
                GameObject sfx = new GameObject("HitSound");
                var a = sfx.AddComponent<AudioSource>();
                a.spatialBlend = 0f;
                a.PlayOneShot(weaponData.hitSound);
                Destroy(sfx, weaponData.hitSound.length);
            }
        }
    }

}
