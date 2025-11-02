using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    public PlayerStats player;
    public Slider healthBar;
    public Slider xpBar;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI killText;
    public TextMeshProUGUI timerText;

    public Image damageVignette; 


    float timer;

    void OnEnable()
    {
        if (player == null)
            player = FindObjectOfType<PlayerStats>();
    }


    void Update()
    {
        healthBar.maxValue = player.maxHealth;
        healthBar.value = player.currentHealth;
        healthText.text = $"{(int)player.currentHealth}/{(int)player.maxHealth}";

        xpBar.maxValue = player.experienceCap;
        xpBar.value = player.experience;

        levelText.text = "LV " + player.level;
        killText.text = player.kills.ToString();

        timer += Time.deltaTime;
        timerText.text = $"{(int)(timer / 60):00}:{(int)(timer % 60):00}";

        float healthPercent = player.currentHealth / player.maxHealth;
        float targetAlpha = Mathf.Lerp(0f, 0.95f, 1f - healthPercent);

        Color c = damageVignette.color;
        c.a = Mathf.Lerp(c.a, targetAlpha, Time.deltaTime * 5f);
        damageVignette.color = c;


    }
}
