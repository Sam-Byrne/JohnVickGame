using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeaponSoundLimiter
{
    private static Dictionary<WeaponScriptableObject, int> activeCounts = new Dictionary<WeaponScriptableObject, int>();

    public static bool CanPlay(WeaponScriptableObject weapon)
    {
        if (weapon == null || weapon.hitSound == null)
            return false;

        if (!activeCounts.ContainsKey(weapon))
            activeCounts[weapon] = 0;

        int limit = Mathf.Max(1, weapon.maxSimultaneousHitSounds);

        if (activeCounts[weapon] >= limit)
            return false;

        activeCounts[weapon]++;

        WeaponSoundLimiterHost.Instance.StartCoroutine(ResetAfterDelay(weapon, weapon.hitSound.length));
        return true;
    }

    private static IEnumerator ResetAfterDelay(WeaponScriptableObject weapon, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (activeCounts.ContainsKey(weapon))
            activeCounts[weapon] = Mathf.Max(0, activeCounts[weapon] - 1);
    }
}
