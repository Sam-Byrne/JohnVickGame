using UnityEngine;

public class WeaponController : MonoBehaviour
{

    protected PlayerMovement pm;

    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    float currentCooldown;

    protected virtual void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        currentCooldown = weaponData.cooldownDuration;
    }

    // Update is called once per frame
    // Virtual keyword tells that it will be overridden in a child class
    // protected means only child classes can access the functions
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            Attack();
        }
    }
    
    protected virtual void Attack()
    {
        currentCooldown = weaponData.cooldownDuration;
    }
}
