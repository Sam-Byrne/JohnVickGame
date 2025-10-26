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
            enemy.TakeDamage(currentDamage);
        }
    }
}
