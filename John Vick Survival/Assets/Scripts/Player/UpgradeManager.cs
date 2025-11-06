using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    PlayerStats player;

    [Header("Weapons that can appear as unlocks")]
    public List<GameObject> unlockableWeapons;

    [Header("Passive Items That Can Appear")]
    public List<PassiveItemScriptableObject> passivePool;


    void Awake()
    {
        instance = this;
        player = FindObjectOfType<PlayerStats>();
    }

    public List<UpgradeOption> GetUpgradeChoices()
    {
        List<UpgradeOption> options = new List<UpgradeOption>();

        float moveSpeedGain = Random.Range(0.15f, 0.30f); // between +0.15 and +0.30 move speed
        options.Add(new UpgradeOption($"+{Mathf.RoundToInt(moveSpeedGain * 100)}% Move Speed",
            () => player.GainMoveSpeed(moveSpeedGain)));

        float healthAmount = Random.Range(10f, 45f); // between 10 and 45 max health
        options.Add(new UpgradeOption($"+{Mathf.RoundToInt(healthAmount)} Max Health",
            () => player.GainMaxHealth(healthAmount)));

        float damageAmount = Random.Range(0.10f, 0.25f); // +10% to +25%
        options.Add(new UpgradeOption(
            $"+{Mathf.RoundToInt(damageAmount * 100)}% Damage",
            () => player.GainDamagePercent(damageAmount)
        ));

        float magnetGain = Random.Range(0.1f, 0.35f); // 10%–35% more radius
        options.Add(new UpgradeOption($"+{Mathf.RoundToInt(magnetGain * 100)}% Magnet",
            () => player.GainMagnet(player.currentMagnet * magnetGain)));

        float atkSpdGain = Random.Range(0.05f, 0.20f); // 5%–20% faster attacks
        options.Add(new UpgradeOption($"+{Mathf.RoundToInt(atkSpdGain * 100)}% Attack Speed",
            () => player.GainAttackSpeed(atkSpdGain)));

        float projectileGain = Random.Range(0.10f, 0.50f); // 10–50% faster
        options.Add(new UpgradeOption($"+{Mathf.RoundToInt(projectileGain * 100)}% Projectile Speed",
            () => player.GainProjectileSpeed(player.currentProjectileSpeed * projectileGain)));
            
        float critChanceGain = Random.Range(0.05f, 0.10f); // +5% to +10%
        options.Add(new UpgradeOption($"+{Mathf.RoundToInt(critChanceGain * 100)}% Crit Chance",
            () => player.GainCritChance(critChanceGain)));

        float critDamageGain = Random.Range(0.1f, 0.3f); // +0.1x to +0.3x multiplier
        options.Add(new UpgradeOption($"+{critDamageGain:F1}x Crit Damage",
            () => player.GainCritDamage(critDamageGain)));

        foreach (var item in passivePool)
        {
            bool hasItem = player.passiveItems.Contains(item);

            if (!hasItem && player.passiveItems.Count < player.maxPassiveSlots)
            {
               // Offer item at level 1
                options.Add(new UpgradeOption($"{item.itemName} (New)", () => player.GainPassiveItem(item)));
            }
            else if (hasItem)
            {
                // Offer item level up
                options.Add(new UpgradeOption($"{item.itemName} (Level Up)", () => player.GainPassiveItem(item)));
            }
        }




        foreach (var w in unlockableWeapons)
        {
            bool hasWeapon = player.spawnedWeapons.Exists(sw => sw.name.Contains(w.name));

            if (!hasWeapon)
            {
                options.Add(new UpgradeOption("Unlock " + w.name, () =>
                {
                    GameObject newW = Instantiate(w, player.transform);
                    player.spawnedWeapons.Add(newW);
                }));
            }
        }

        // Pick 3 random upgrades
        List<UpgradeOption> selected = new List<UpgradeOption>();
        List<UpgradeOption> pool = new List<UpgradeOption>(options);

        int numberToPick = Mathf.Min(3, pool.Count);

        for (int i = 0; i < numberToPick; i++)
        {
            int index = Random.Range(0, pool.Count);
            selected.Add(pool[index]);
            pool.RemoveAt(index); // prevents duplicates
        }

        return selected;
    }
}

public class UpgradeOption
{
    public string text;
    public System.Action apply;

    public UpgradeOption(string text, System.Action apply)
    {
        this.text = text;
        this.apply = apply;
    }
}
