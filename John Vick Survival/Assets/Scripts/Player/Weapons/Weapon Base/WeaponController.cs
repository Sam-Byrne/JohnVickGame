using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponScriptableObject weaponData;

    protected PlayerMovement pm;
    float cooldownTimer;

    protected virtual void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        cooldownTimer = weaponData.CooldownDuration;
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0f)
        {
            Attack();
            cooldownTimer = weaponData.CooldownDuration;
        }
    }

    protected virtual void Attack() { }
}
