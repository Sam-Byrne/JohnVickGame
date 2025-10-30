using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    // current stats of the enemy
    public float currentMoveSpeed;
    public float currentHealth;
    public float currentDamage;
    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
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
        currentMoveSpeed *= 1f + ((multiplier - 1f) / 2f); // small speed boost
    }
}
