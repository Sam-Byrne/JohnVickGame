using UnityEngine;

public class RadianceController : WeaponController 
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedRadiance = Instantiate(prefab);
        spawnedRadiance.transform.position = transform.position;
        
    }

}
