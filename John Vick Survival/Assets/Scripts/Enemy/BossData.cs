using UnityEngine;

[CreateAssetMenu(fileName = "BossMan", menuName = "Game/Boss")]
public class BossData : ScriptableObject
{
    public string bossName;
    public GameObject prefab;
    public AudioClip bossMusic;
    public float spawnTimeMinutes = 5f;
}
