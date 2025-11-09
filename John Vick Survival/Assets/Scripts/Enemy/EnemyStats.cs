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

    public System.Action onEnemyDeath;





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

    public void TakeDamage(float dmg, bool crit, WeaponScriptableObject sourceWeapon)
    {
        GetComponent<EnemyFlash>()?.Flash();

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

        if (sourceWeapon != null)
        {
            if (sourceWeapon.hitSound != null && WeaponSoundLimiter.CanPlay(sourceWeapon))
                PlaySound(sourceWeapon.hitSound, sourceWeapon.hitVolume);

            if (crit && sourceWeapon.critSound != null && WeaponSoundLimiter.CanPlay(sourceWeapon))
            {
                PlaySound(sourceWeapon.critSound, sourceWeapon.critVolume);
            }
        }



        if (damageSound != null)
            AudioSource.PlayClipAtPoint(damageSound, transform.position);


        if (currentHealth <= 0)
        {
            Kill();
            return;
        }
    }

    void PlaySound(AudioClip clip, float volume)
    {
        GameObject sfx = new GameObject("HitSound");
        var a = sfx.AddComponent<AudioSource>();
        a.spatialBlend = 0f;
        a.volume = volume;
        a.PlayOneShot(clip);
        Destroy(sfx, clip.length);
    }



    public void Kill()
    {
        if (deathSound != null)
            AudioSource.PlayClipAtPoint(deathSound, transform.position);

        if (player != null)
            player.AddKill();

        if (HUDController.Instance != null && HUDController.Instance.IsBossHealthVisible(this))
        {
            HUDController.Instance.HideBossHealth();
            AudioManager.Instance.PlayRandomLevelMusic(); 
        }

        onEnemyDeath?.Invoke();
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
