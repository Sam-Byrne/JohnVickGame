using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    [Header("References")]
    public PlayerStats playerStats;     // assign in Inspector

    [Header("UI Elements")]
    public Slider healthBar;
    public TextMeshProUGUI healthText;
    public Slider xpBar;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI killText;

    private float timer = 0f;

    void Start()
    {
        if (playerStats == null)
            playerStats = FindObjectOfType<PlayerStats>();

        // Initialize
        healthBar.value = 1;
        xpBar.value = 0;
        levelText.text = "Lv 1";
        timerText.text = "00:00";
        killText.text = "Kills: 0";
        healthText.text = "HP: 0 / 0";
    }

    void Update()
    {
        if (playerStats == null) return;


        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";


        float currentHP = playerStats.GetCurrentHealth();
        float maxHP = playerStats.GetMaxHealth();

        healthBar.value = Mathf.Clamp01(currentHP / maxHP);
        healthText.text = $"{Mathf.CeilToInt(currentHP)} / {Mathf.CeilToInt(maxHP)}";


        xpBar.value = (float)playerStats.experience / playerStats.experienceCap;


        levelText.text = $"Lv {playerStats.level}";


        killText.text = $"Kills: {playerStats.kills}";
    }
}
