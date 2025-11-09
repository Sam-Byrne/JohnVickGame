using UnityEngine;

public class XP : Pickup, ICollectable 
{
    public int xpValue = 1;
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
            player.GetComponent<PlayerStats>().IncreaseExperience(xpValue);  

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
