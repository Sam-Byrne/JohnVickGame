using UnityEngine;

public class MeleeWeaponBehaviour : MonoBehaviour
{
    public float destroyAfterSeconds;
    public WeaponScriptableObject weaponData;

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }
}
