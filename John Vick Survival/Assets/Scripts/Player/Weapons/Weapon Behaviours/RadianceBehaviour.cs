using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RadianceBehaviour : MeleeWeaponBehaviour
{

    List<GameObject> markedEnemies;
    protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();
    }


    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") && !markedEnemies.Contains(col.gameObject))
        {
            var enemy = col.GetComponent<EnemyStats>();
            var p = FindObjectOfType<PlayerStats>();

            float dmg = currentDamage * p.damageMultiplier;

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
