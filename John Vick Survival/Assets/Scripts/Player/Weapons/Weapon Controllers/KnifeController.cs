using UnityEngine;

public class KnifeController : WeaponController
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
        if (pm == null) pm = FindObjectOfType<PlayerMovement>();
        Vector2 dir = pm.LastAimDir;

        GameObject knife = Instantiate(weaponData.Prefab, transform.position, Quaternion.identity);
        knife.GetComponent<KnifeBehaviour>().Initialize(weaponData.Damage, dir, weaponData.Speed); 

        if (attackClip != null) audioSource.PlayOneShot(attackClip, 0.35f);
    }


}
