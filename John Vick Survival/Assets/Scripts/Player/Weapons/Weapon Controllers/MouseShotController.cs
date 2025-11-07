using UnityEngine;

public class MouseShotController : WeaponController
{

    public AudioSource audioSource;
    public AudioClip attackClip;
    protected override void Start()
    {
        base.Start();
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
    }
    protected override void Attack()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direction = (mousePos - transform.position).normalized;

        GameObject proj = Instantiate(weaponData.Prefab, transform.position, Quaternion.identity);
        proj.GetComponent<MouseProjectileBehaviour>().Initialize(direction);
        if (attackClip != null) audioSource.PlayOneShot(attackClip, 0.35f);
    }
}
