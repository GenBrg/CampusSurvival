using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class Health : Damagable
{
    public float maxHealth;
    public float currentHealth;
    public delegate void OnHealthChange(float currentHealth, float maxHealth);

    public OnHealthChange onHealthChange;
    public UnityAction onHeal;
    public UnityAction onDamaged;
    public UnityAction onDie;

    private void Die()
    {
        currentHealth = 0.0f;
        if (onHealthChange != null)
        {
            onHealthChange(currentHealth, maxHealth);
        }
        onDie();
        Destroy(gameObject);
    }

    public void Heal(float amount)
    {
        if (amount <= 0.0f || currentHealth == maxHealth)
        {
            return;
        }

        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);

        if (onHealthChange != null)
        {
            onHealthChange(currentHealth, maxHealth);
        }

        if (onHeal != null)
        {
            onHeal();
        }
    }

    public override void OnDamage(float damage)
    {
        if (damage <= 0.0f)
        {
            return;
        }

        if (onDamaged != null)
        {
            onDamaged();
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
