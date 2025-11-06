using UnityEngine;
using System.Collections;

public class EnemyFlash : MonoBehaviour
{
    SpriteRenderer sr;
    Color originalColor;
    public float flashDuration = 0.1f;

    void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalColor = sr.color;
    }

    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        sr.color = originalColor;
    }
}
