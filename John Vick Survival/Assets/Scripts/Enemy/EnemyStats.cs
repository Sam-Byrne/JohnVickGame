using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    private PlayerStats player;

    [Header("Audio Settings")]
    public AudioClip damageSound;
    public AudioClip deathSound;
    private AudioSource audioSource;


    [Header("Damage Popup")]
    public GameObject damagePopupPrefab;




    // current stats
    public float currentMoveSpeed;
    public float currentHealth;
    public float currentDamage;


    public Material dissolveMat; 
    public float dissolveTime = 1f;



    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
        player = FindObjectOfType<PlayerStats>();

        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
    }

    public void TakeDamage(float dmg)
    {
        GetComponent<EnemyFlash>()?.Flash();
        bool crit = dmg > enemyData.Damage; 

        currentHealth -= dmg;

        // spawn damage popup
        if (damagePopupPrefab != null)
        {
            var popup = Instantiate(
                damagePopupPrefab,
                transform.position + Vector3.up * 0.6f,
                Quaternion.identity
            );

            popup.GetComponent<DamagePopup>().Setup(dmg, crit);
        }

        if (damageSound != null)
            AudioSource.PlayClipAtPoint(damageSound, transform.position);

        if (currentHealth <= 0)
        {
            Kill();
            return;
        }
    }



    public void Kill()
    {
        // play death sound before destroying
        if (deathSound != null)
            AudioSource.PlayClipAtPoint(damageSound, transform.position);
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
