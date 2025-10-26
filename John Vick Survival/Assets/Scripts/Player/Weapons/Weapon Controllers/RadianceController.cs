using UnityEngine;

public class RadianceController : WeaponController 
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedRadiance = Instantiate(weaponData.Prefab);
        spawnedRadiance.transform.position = transform.position;
        spawnedRadiance.transform.parent = transform;

    }

}
