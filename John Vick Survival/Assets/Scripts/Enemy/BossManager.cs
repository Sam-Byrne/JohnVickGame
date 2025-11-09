using UnityEngine;

public class BossManager : MonoBehaviour
{
    [System.Serializable]
    public class BossData
    {
        public GameObject bossPrefab;
        public string bossName;
        public AudioClip bossMusic;
        public float spawnAtMinutes;
    }

    public BossData[] bosses;
    public AudioClip normalMusic;

    private bool[] spawned;
    private AudioManager audioManager;

    void Start()
    {
        spawned = new bool[bosses.Length];
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        float minutes = HUDController.Instance.Timer / 60f;

        for (int i = 0; i < bosses.Length; i++)
        {
            if (!spawned[i] && minutes >= bosses[i].spawnAtMinutes)
            {
                SpawnBoss(bosses[i]);
                spawned[i] = true;
            }
        }
    }

    void SpawnBoss(BossData data)
    {
        GameObject bossObj = Instantiate(data.bossPrefab, GetSpawnPosition(), Quaternion.identity);
        EnemyStats stats = bossObj.GetComponent<EnemyStats>();

        HUDController.Instance.ShowBossHealth(stats, data.bossName);

        if (audioManager != null)
            audioManager.musicSource.clip = data.bossMusic;
        audioManager.musicSource.loop = true;
        audioManager.musicSource.Play();



        stats.onEnemyDeath += () =>
        {
            HUDController.Instance.HideBossHealth();
            if (audioManager != null)
                audioManager.PlayRandomLevelMusic();
        };
    }

    Vector3 GetSpawnPosition()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        return player.transform.position + new Vector3(Random.Range(6, 10), Random.Range(6, 10));
    }
}
