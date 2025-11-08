using UnityEngine;

public class HealthPotion : Pickup, ICollectable  // was: MonoBehaviour
{
    public int healAmount = 10;
    public float pullSpeed = 8f;

    Transform player;
    bool isBeingPulled = false;

    public void Collect()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        isBeingPulled = true;
    }

    void Update()
    {
        if (!isBeingPulled) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            pullSpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, player.position) < 0.2f)
        {
            PlayerStats p = player.GetComponent<PlayerStats>();
            p.currentHealth = Mathf.Min(p.currentHealth + healAmount, p.maxHealth);

            // juicy pickup sound
            if (pickupSound != null)
            {
                var sfx = new GameObject("PickupSFX").AddComponent<AudioSource>();
                sfx.spatialBlend = 0f;
                sfx.volume = pickupVolume;
                sfx.pitch = Random.Range(0.92f, 1.08f);
                sfx.PlayOneShot(pickupSound);
                Destroy(sfx.gameObject, pickupSound.length);
            }

            Destroy(gameObject);
        }
    }
}
