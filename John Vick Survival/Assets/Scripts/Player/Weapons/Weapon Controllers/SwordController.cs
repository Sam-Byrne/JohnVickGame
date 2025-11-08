using UnityEngine;

public class SwordController : WeaponController
{
    protected override void Attack()
    {
        base.Attack(); 

        if (weaponData == null || weaponData.Prefab == null)
            return;

        Vector2 dir = pm.LastAimDir;
        if (dir.sqrMagnitude < 0.01f)
            dir = Vector2.right;

        GameObject swordGO = Instantiate(weaponData.Prefab, pm.transform);

        SwordBehaviour sword = swordGO.GetComponent<SwordBehaviour>();
        if (sword != null)
            sword.weaponData = weaponData;

        float spawnOffset = 0.5f;
        swordGO.transform.localPosition = (Vector3)dir.normalized * spawnOffset;

        sword.SetDirection(pm.lastFacing);
    }


}
