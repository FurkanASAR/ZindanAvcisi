using System;
using System.Diagnostics;

public class HealthSystem
{
    public event EventHandler OnHealthChanged;   


    private float health;
    private float maxHealth;

    public HealthSystem(float maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(float heal)
    {
        health += heal;
        if(health > 100)
        {
            health = 100;
        }
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }
    public float GetHealth()
    {
        return health;      
    }
}
