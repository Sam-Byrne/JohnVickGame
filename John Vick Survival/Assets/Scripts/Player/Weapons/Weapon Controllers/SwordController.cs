using UnityEngine;

public class SwordController : WeaponController
{
    protected override void Attack()
    {
        base.Attack();
        Debug.Log("SWORD ATTACK TRIGGERED");

        if (weaponData == null || weaponData.Prefab == null) return;
        GameObject swordGO = Instantiate(weaponData.Prefab, pm.transform);
        float spawnOffset = 0.5f; 

        Vector2 dir = pm.LastAimDir;
        if (dir.sqrMagnitude < 0.01f)
            dir = Vector2.right;

        swordGO.transform.localPosition = (Vector3)dir.normalized * spawnOffset;


        var sword = swordGO.GetComponent<SwordBehaviour>();
        if (sword != null)
        {
            sword.SetDirection(pm.LastAimDir);
        }
    }
}
