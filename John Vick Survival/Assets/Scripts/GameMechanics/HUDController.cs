using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;

public class HUDController : MonoBehaviour
{
    public PlayerStats player;
    public Slider healthBar;
    public Slider xpBar;
    public Image damageFlash;

    bool stopTimer = false;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI killText;
    public TextMeshProUGUI timerText;

    public Image damageVignette;

    [Header("Blessing HUD")]
    public Transform passiveItemBar;
    public GameObject passiveItemSlotPrefab;

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

        if (!stopTimer)
            timer += Time.deltaTime;

        timerText.text = $"{(int)(timer / 60):00}:{(int)(timer % 60):00}";

        float healthPercent = player.currentHealth / player.maxHealth;
        float targetAlpha = Mathf.Lerp(0f, 0.95f, 1f - healthPercent);

        Color c = damageVignette.color;
        c.a = Mathf.Lerp(c.a, targetAlpha, Time.deltaTime * 5f);
        damageVignette.color = c;


    }

    public void FlashDamage()
    {
        StopAllCoroutines();
        StartCoroutine(DamageFlashRoutine());
    }

    IEnumerator DamageFlashRoutine()
    {
        Color c = damageFlash.color;
        c.a = 0.8f;
        damageFlash.color = c;

        float t = 0f;
        while (t < 0.25f)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0.8f, 0f, t / 0.25f);
            damageFlash.color = c;
            yield return null;
        }
    }


    public void RefreshPassiveHUD(PlayerStats player)
    {
        foreach (Transform child in passiveItemBar)
            Destroy(child.gameObject);

        foreach (var item in player.passiveItems)
        {
            var slot = Instantiate(passiveItemSlotPrefab, passiveItemBar);
            slot.GetComponent<Image>().sprite = item.icon;

            int level = player.GetPassiveLevel(item);

            TextMeshProUGUI lvlText = slot.GetComponentInChildren<TextMeshProUGUI>();
            lvlText.text = level.ToString();
        }
    }

    public void StopTimer()
    {
        stopTimer = true;
    }
}
