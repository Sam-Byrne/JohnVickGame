using System.Collections.Generic;

public static class BossSpawnState
{
    private static readonly HashSet<BossData> spawned = new HashSet<BossData>();

    public static bool HasSpawned(BossData data) => spawned.Contains(data);
    public static void MarkSpawned(BossData data) => spawned.Add(data);
}
