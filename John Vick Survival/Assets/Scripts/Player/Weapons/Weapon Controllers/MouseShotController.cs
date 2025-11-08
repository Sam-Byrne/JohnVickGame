using UnityEngine;

public class MouseShotController : WeaponController
{

    protected override void Start()
    {
        base.Start();
    }
    protected override void Attack()
    {
        base.Attack();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direction = (mousePos - transform.position).normalized;

        GameObject proj = Instantiate(weaponData.Prefab, transform.position, Quaternion.identity);
        proj.GetComponent<MouseProjectileBehaviour>().Initialize(direction);
        if (attackClip != null) audioSource.PlayOneShot(attackClip, 0.35f);
    }
}
