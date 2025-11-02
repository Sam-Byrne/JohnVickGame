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

        
        option1Text.text = "Upgrade A";
        option2Text.text = "Upgrade B";
        option3Text.text = "Upgrade C";

        
        option1.onClick.RemoveAllListeners();
        option2.onClick.RemoveAllListeners();
        option3.onClick.RemoveAllListeners();


        option1.onClick.AddListener(() => ChooseUpgrade());
        option2.onClick.AddListener(() => ChooseUpgrade());
        option3.onClick.AddListener(() => ChooseUpgrade());
    }

    void ChooseUpgrade()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
