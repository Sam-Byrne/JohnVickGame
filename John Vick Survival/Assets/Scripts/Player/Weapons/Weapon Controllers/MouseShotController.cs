using UnityEngine;

public class MouseShotController : WeaponController
{
    protected override void Attack()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direction = (mousePos - transform.position).normalized;

        GameObject proj = Instantiate(weaponData.Prefab, transform.position, Quaternion.identity);
        proj.GetComponent<MouseProjectileBehaviour>().Initialize(direction);
    }
}
