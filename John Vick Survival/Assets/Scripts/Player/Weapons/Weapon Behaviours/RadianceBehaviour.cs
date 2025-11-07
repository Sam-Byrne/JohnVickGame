using UnityEngine;
using System.Collections.Generic;

public class RadianceBehaviour : MonoBehaviour
{
    public float destroyAfterSeconds;
    public WeaponScriptableObject weaponData;

    float baseDamage;

    List<GameObject> markedEnemies;

    void Awake()
    {
        baseDamage = weaponData.Damage;
        markedEnemies = new List<GameObject>();
    }

    void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") && !markedEnemies.Contains(col.gameObject))
        {
            var enemy = col.GetComponent<EnemyStats>();
            var p = FindObjectOfType<PlayerStats>();

            float dmg = baseDamage * p.damageMultiplier;

            bool crit = false;
            if (Random.value <= p.critChance)
            {
                dmg *= p.critDamage;
                crit = true;
            }

            enemy.TakeDamage(dmg, crit);
            markedEnemies.Add(col.gameObject);
        }
    }
}
