using UnityEngine;

public class MouseProjectileBehaviour : MonoBehaviour
{
    public float destroyAfterSeconds = 4f;
    public WeaponScriptableObject weaponData;

    float currentDamage;
    float currentSpeed;
    int currentPierce;

    Vector2 direction;

    void Awake()
    {
        var p = FindObjectOfType<PlayerStats>();

        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed + (p != null ? p.currentProjectileSpeed : 0f);
        currentPierce = weaponData.Pierce;

        Destroy(gameObject, destroyAfterSeconds);
    }

    public void Initialize(Vector2 dir)
    {
        direction = dir.normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        transform.Translate(direction * currentSpeed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out EnemyStats enemy))
        {
            var p = FindObjectOfType<PlayerStats>();
            float dmg = currentDamage * p.damageMultiplier;
            
            bool crit = false;
            if (Random.value <= p.critChance)
            {
                dmg *= p.critDamage;
                crit = true;
            }

            enemy.TakeDamage(dmg, crit);

            currentPierce--;
            if (currentPierce <= 0)
                Destroy(gameObject);
        }
    }
}
