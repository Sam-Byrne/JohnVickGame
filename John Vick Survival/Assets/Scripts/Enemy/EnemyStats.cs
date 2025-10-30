using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    private PlayerStats player;

    [Header("Audio Settings")]
    public AudioClip damageSound;
    public AudioClip deathSound;
    private AudioSource audioSource;

    // current stats
    public float currentMoveSpeed;
    public float currentHealth;
    public float currentDamage;

    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
        player = FindObjectOfType<PlayerStats>();

        // Add or get an AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        // play damage sound if available
        if (damageSound != null)
            audioSource.PlayOneShot(damageSound);

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        // play death sound before destroying
        if (deathSound != null)
            AudioSource.PlayClipAtPoint(deathSound, transform.position);

        if (player != null)
        {
            player.AddKill();
        }

        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }

    public void ApplyScaling(float multiplier)
    {
        currentHealth *= multiplier;
        currentDamage *= multiplier;
        currentMoveSpeed *= 1f + ((multiplier - 1f) / 2f);
    }
}
