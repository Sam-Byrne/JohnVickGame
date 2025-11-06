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

            float dmg = currentDamage;
            if (p != null) dmg *= p.damageMultiplier;
            if (p != null && Random.value <= p.critChance)
                dmg *= p.critDamage;

            enemy.TakeDamage(dmg);
            markedEnemies.Add(col.gameObject);
        }
    }





}
