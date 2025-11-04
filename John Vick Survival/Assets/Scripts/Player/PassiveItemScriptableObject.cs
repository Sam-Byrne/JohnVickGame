using UnityEngine;

[CreateAssetMenu(fileName = "Passive Item", menuName = "ScriptableObjects/Passive Item")]
public class PassiveItemScriptableObject : ScriptableObject
{
    public string itemName;

    [TextArea] 
    public string description;

    public Sprite icon;

    [Header("Passive Type")]
    public PassiveType passiveType;

    [Header("Upgrade Values Per Level")]
    public float[] upgradeValues; // Example: { 0.10f, 0.15f, 0.20f }

    public enum PassiveType
    {
        MaxHealth,
        Damage,
        MoveSpeed,
        AttackSpeed,
        CritChance,
        CritDamage,
        Magnet
    }
}
