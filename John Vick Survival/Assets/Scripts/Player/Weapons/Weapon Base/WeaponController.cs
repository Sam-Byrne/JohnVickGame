using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponScriptableObject weaponData;

    protected PlayerMovement pm;
    float cooldownTimer;
    public AudioSource audioSource;
    public AudioClip attackClip;

    protected virtual void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        cooldownTimer = weaponData.CooldownDuration;
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime * FindObjectOfType<PlayerStats>().attackSpeedMultiplier;
        if (cooldownTimer <= 0f)
        {
            Attack();
            cooldownTimer = weaponData.CooldownDuration;
        }
    }

    protected virtual void Attack()
    {
        if (attackClip != null)
            audioSource.PlayOneShot(attackClip, 0.35f);
    }
}
