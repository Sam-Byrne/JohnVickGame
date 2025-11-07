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
    public float[] upgradeValues;

    public enum PassiveType
    {
        MaxHealth,
        DamageMultiplier,
        MoveSpeed,
        AttackSpeed,
        CritChance,
        CritDamage,
        Magnet,
        Recovery
    }
}
