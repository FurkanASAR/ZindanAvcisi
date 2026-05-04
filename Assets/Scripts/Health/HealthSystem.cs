using System;
using System.Diagnostics;
using UnityEngine;

public class HealthSystem
{
    public event EventHandler OnHealthChanged;
    public event EventHandler OnDeath;

    private GameObject owner;
    private float health;
    private float maxHealth;

    public HealthSystem(GameObject owner, float maxHealth)
    {
        this.maxHealth = maxHealth;
        this.owner = owner;
        health = maxHealth;

        OnDeath += (sender, e) => Death();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(float heal)
    {
        health += heal;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }
    public float GetHealth()
    {
        return health;      
    }

    public void Death()
    {
        owner.SetActive(false);
    }
}
