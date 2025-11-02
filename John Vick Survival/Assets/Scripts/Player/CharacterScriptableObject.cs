using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "ScriptableObjects/Character")]
public class CharacterScriptableObject : ScriptableObject
{
    [Header("Character Base Stats")]
    public float MaxHealth;
    public float Damage;
    public float ProjectileSpeed;
    public float Recovery;
    public float Magnet;
    public float MoveSpeed;

    [Header("Starting Weapon")]
    public WeaponController StartingWeapon;
}
