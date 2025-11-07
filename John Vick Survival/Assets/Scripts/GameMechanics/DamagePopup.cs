using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public TextMeshPro text;

    float lifetime = 0.6f;
    Vector3 move = new Vector3(0, 1.4f, 0);

    void Awake()
    {
        if (text == null)
            text = GetComponent<TextMeshPro>();
    }

    public void Setup(float damage, bool crit)
    {
        text.text = damage.ToString("0.#");
        if (crit) text.color = Color.yellow;   // crits are yellow
    }

    void Update()
    {
        transform.position += move * Time.deltaTime;
        lifetime -= Time.deltaTime;

        text.alpha = lifetime / 0.6f;

        if (lifetime <= 0f)
            Destroy(gameObject);
    }
}
