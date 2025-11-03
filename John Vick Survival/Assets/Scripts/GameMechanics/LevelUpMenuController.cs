using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpMenuController : MonoBehaviour
{
    public Button option1;
    public Button option2;
    public Button option3;

    public TextMeshProUGUI option1Text;
    public TextMeshProUGUI option2Text;
    public TextMeshProUGUI option3Text;

    void Start()
    {
        gameObject.SetActive(false);
        PlayerStats.OnLevelUpUI += ShowMenu;
    }

    void ShowMenu()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;

        var upgrades = UpgradeManager.instance.GetUpgradeChoices();

        // Display correct text
        option1Text.text = upgrades[0].text;
        option2Text.text = upgrades[1].text;
        option3Text.text = upgrades[2].text;

        // Remove old listeners
        option1.onClick.RemoveAllListeners();
        option2.onClick.RemoveAllListeners();
        option3.onClick.RemoveAllListeners();

        // Link correct actions
        option1.onClick.AddListener(() => ApplyUpgrade(upgrades[0]));
        option2.onClick.AddListener(() => ApplyUpgrade(upgrades[1]));
        option3.onClick.AddListener(() => ApplyUpgrade(upgrades[2]));
    }

    void ApplyUpgrade(UpgradeOption upgrade)
    {
        upgrade.apply.Invoke();
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
