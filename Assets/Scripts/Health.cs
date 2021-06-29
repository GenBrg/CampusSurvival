using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Damagable
{
    public float maxHealth;
    public float currentHealth;
    public delegate void OnHealthChange(float currentHealth, float maxHealth);
    public OnHealthChange onHealthChange;

    private void Die()
    {
        currentHealth = 0.0f;
        if (onHealthChange != null)
        {
            onHealthChange(currentHealth, maxHealth);
        }
        Destroy(gameObject);
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        if (onHealthChange != null)
        {
            onHealthChange(currentHealth, maxHealth);
        }
    }

    public override void OnDamage(float damage)
    {
        if (damage <= 0.0f)
        {
            return;
        }

        if (currentHealth <= damage)
        {
            Die();
            return;
        }

        currentHealth -= damage;
        if (onHealthChange != null)
        {
            onHealthChange(currentHealth, maxHealth);
        }
    }
}
