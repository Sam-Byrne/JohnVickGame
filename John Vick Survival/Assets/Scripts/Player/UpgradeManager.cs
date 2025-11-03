using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    PlayerStats player;

    [Header("Weapons that can appear as unlocks")]
    public List<GameObject> unlockableWeapons;

    void Awake()
    {
        instance = this;
        player = FindObjectOfType<PlayerStats>();
    }

    public List<UpgradeOption> GetUpgradeChoices()
    {
        List<UpgradeOption> options = new List<UpgradeOption>();

        float healthAmount = Random.Range(10f, 35f);
        options.Add(new UpgradeOption($"+{Mathf.RoundToInt(healthAmount)} Max Health", 
            () => player.GainMaxHealth(healthAmount)));
            
        float damageAmount = Random.Range(0.1f, 0.65f); // 10%–65% more damage
        options.Add(new UpgradeOption($"+{Mathf.RoundToInt(damageAmount * 100)}% Damage", 
            () => player.GainDamage(player.currentDamage * damageAmount)));

        float magnetGain = Random.Range(0.1f, 0.35f); // 10%–35% more radius
        options.Add(new UpgradeOption($"+{Mathf.RoundToInt(magnetGain * 100)}% Magnet", 
            () => player.GainMagnet(player.currentMagnet * magnetGain)));

        float projectileGain = Random.Range(0.10f, 0.40f); // 10–40% faster
        options.Add(new UpgradeOption($"+{Mathf.RoundToInt(projectileGain * 100)}% Projectile Speed", 
            () => player.GainProjectileSpeed(player.currentProjectileSpeed * projectileGain)));


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
        for (int i = 0; i < 3; i++)
            selected.Add(options[Random.Range(0, options.Count)]);

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
