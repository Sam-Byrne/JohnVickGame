using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeathScreenController : MonoBehaviour
{
    public SpriteRenderer blackout;  
    public GameObject gameOverPanel;   
    public float fadeSpeed = 1f; 

    public void TriggerGameOver()
    {
        StartCoroutine(FadeAndShow());
    }

    private IEnumerator FadeAndShow()
    {
        // Ensure blackout starts fully transparent
        Color c = blackout.color;
        c.a = 0f;
        blackout.color = c;

        // Fade to black
        while (c.a < 1f)
        {
            c.a += Time.deltaTime * fadeSpeed;
            blackout.color = c;
            yield return null;
        }

        gameOverPanel.SetActive(true);

        yield break;
    }

    public void ReturnToMenu()
    {
        FindObjectOfType<MusicFadeController>().ResetMusic();
        SceneManager.LoadScene("Menu");
    }
}
