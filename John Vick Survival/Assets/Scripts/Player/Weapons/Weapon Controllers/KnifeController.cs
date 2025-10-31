using UnityEngine;

public class KnifeController : WeaponController
{
    [Header("Knife Audio")]
    public AudioClip knifeThrowSound;

    protected override void Attack()
    {
        base.Attack();

        GameObject spawnedKnife = Instantiate(weaponData.Prefab);
        spawnedKnife.transform.position = transform.position;
        spawnedKnife.GetComponent<KnifeBehaviour>().DirectionChecker(pm.lastMovedVector);

        if (knifeThrowSound != null)
            AudioSource.PlayClipAtPoint(knifeThrowSound, transform.position);
    }
}
    