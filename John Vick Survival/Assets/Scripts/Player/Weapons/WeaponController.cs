using UnityEngine;

public class WeaponController : MonoBehaviour
{


    [Header("Weapon Stats")]
    public GameObject prefab;
    public float damage;
    public float speed;
    public float cooldownDuration;
    float currentCooldown;
    public int pierce;
    protected virtual void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        currentCooldown = cooldownDuration;
    }

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
        currentCooldown = cooldownDuration;
    }
}
