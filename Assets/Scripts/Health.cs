using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Generic health component for player/enemies.
/// </summary>
public class Health : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    public UnityEvent onDeath;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int amount)
    {
        if (currentHP <= 0) return;
        currentHP = Mathf.Max(0, currentHP - amount);
        if (currentHP == 0) Die();
    }

    public void Heal(int amount)
    {
        if (currentHP <= 0) return;
        currentHP = Mathf.Min(maxHP, currentHP + amount);
    }

    private void Die()
    {
        onDeath?.Invoke();
        gameObject.SetActive(false); // pooled-friendly
    }
}